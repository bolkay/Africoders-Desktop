using AfricodersProject.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AfricodersProject
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window
    {
        /// <summary>
        /// Unique token for the logged in user.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// For the best experience, the user may chose to simply load the page being viewed in the browser.
        /// </summary>
        public string PostSlug { get; set; }

        private PostHelper postHelper;
        public ForumWindow()
        {
            InitializeComponent();
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {

            Button editBtn = sender as Button;

            dynamic data = (dynamic)editBtn.DataContext;
            //Button to edit a comment.
            try
            {
                if (data != null)
                {
                    //Edit the token.
                    string tkBuild = "Bearer " + Token;
                    //Get the id
                    string id = data.id.ToString();

                    PostWindow postWindow = new PostWindow();
                    //Employ the status control for changing comments.
                    postWindow.theStatusPostControl.Visibility = Visibility.Visible;
                    postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
                    postWindow.theLinksControl.Visibility = Visibility.Hidden;

                    //MessageBox.Show(id + " " + Token);
                    //First clear the placeholder text
                    postWindow.theStatusPostControl.RTBox.Document.Blocks.Clear();
                    postWindow.titleText.Text = "Edit your comment.";
                    postWindow.theStatusPostControl.RTBox.AppendText(data.body);
                    postWindow.ShowDialog();

                    string body = postWindow.theStatusPostControl.Body;

                    string endPoint = @"https://api.africoders.com/v1/edit/comment?id=" + id + "&body=" + body;

                    postHelper = new PostHelper(endPoint);

                    bottomPageText.Text = "Attempting to update comment. Please wait.";
                    await postHelper.MakePostAsync(tkBuild);

                    if (postHelper.StatusPostSuccessful)
                    {
                        bottomPageText.Text = "Comment successfully updated.";
                    }
                    else
                    {
                        bottomPageText.Text = "Unable to update comment at this time.";
                    }
                }
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            dynamic data = (dynamic)btn.DataContext;

            if (data != null)
            {
                try
                {

                    string tkBuild = "Bearer " + Token;

                    string id = data.id.ToString();

                    postHelper = new PostHelper(@"https://api.africoders.com/v1/del/comment?id=" + id);

                    MessageBoxResult messageBoxResult = MessageBox.Show("Delete your comment?", "Comment Deletion.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (messageBoxResult)
                    {
                        case MessageBoxResult.Yes:
                            bottomPageText.Text = "Deleting Comment..";
                            await postHelper.MakePostAsync(tkBuild);
                            if (postHelper.StatusPostSuccessful)
                                bottomPageText.Text = "Comment Deleted.";
                            else
                                bottomPageText.Text = "Unable to delete comment at this time.";
                            break;
                        case MessageBoxResult.No:
                            return;
                    }
                }
                catch (Exception d)
                {
                    MessageBox.Show(d.Message);
                }
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadLinkButton_Click(object sender, RoutedEventArgs e)
        {
            Process process = Process.Start(PostSlug);


        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

            try { DragMove(); }
            catch { return; }
        }
    }
}
