using AfricodersProject.AfricoderLinksModel;
using AfricodersProject.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace AfricodersProject.Africoders_Custom_User_Controls
{

    /// <summary>
    /// Interaction logic for LinksControl.xaml
    /// </summary>
    public partial class LinksControl : UserControl
    {
        public int LoggedID { get; set; }
        private string endpoint = @"https://api.africoders.com/v1/posts?category=link&order=published_at|desc&limit=10&page=";
        /// <summary>
        /// Unique token for the authenticatd user.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Endpoint for link postings.
        /// </summary>
        private string LinkPostEndpoint = @"https://api.africoders.com/v1/post/link";
        /// <summary>
        /// Reference to the PostHelper class.
        /// </summary>
        private PostHelper postHelper;
        /// <summary>
        /// Reference to the pagination helper.
        /// </summary>
        private PaginationHelper paginationHelper;
        /// <summary>
        /// Index for pagination.
        /// </summary>
        private int currentPageIndex = 1;

        /// <summary>
        /// Reference to the comment poster.
        /// </summary>
        private CommentPoster commentPoster;
        public LinksControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Make a status post.
        /// </summary>
        /// <param name="postBody">Message to be posted to the endpoint.</param>
        private async void MakePost(string postBody, string postTitle, string url)
        {
            string tokenBuilder = "Bearer " + Token;
            LinkPostEndpoint = LinkPostEndpoint + "?title=" + postTitle + "&body=" + postBody + "&url=" + url;

            postHelper = new PostHelper(LinkPostEndpoint);

            await postHelper.MakePostAsync(tokenBuilder);
            if (postHelper.StatusPostSuccessful)
            {
                loadingTextBlock.Text = "Link Successfully Posted.";
                //MessageBox.Show("Posted.");
                //Refresh the control.
                Refresh();
            }
            else
            {
                loadingTextBlock.Text = "Unable to post your link at this time. Please try again";
                //MessageBox.Show("Unsucessful Operation.");
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadingTextBlock.Text = "Africoders Jobs";
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex++;
            LinksUpdate();
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex--;
            LinksUpdate();
        }
        private async void LinksUpdate()
        {
            try
            {
                loadingTextBlock.Text = $"Loading page {currentPageIndex}. Hold on a moment please.";
                paginationHelper = new PaginationHelper(endpoint);

                Datum[] data = await paginationHelper.GetNextLinkPosts(currentPageIndex, LoggedID);

                if (data != null)
                {
                    foreach (var result in data)
                    {
                        string date = result.created.date;


                        string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                        result.created.date = convertedTime;
                    }
                    //Finally, set the data
                    TheListBox.ItemsSource = data;
                    //MessageBox.Show("loaded");
                    loadingTextBlock.Text = $"Finished loading page {currentPageIndex}";
                    //dispose the helper
                    paginationHelper.Dispose();
                }
                else
                {
                    loadingTextBlock.Text = "Unable to load the page. Consider loading the previous page";
                    //MessageBox.Show("Unable to load the page. Consider loading the previous page");
                }
            }
            catch
            {
                MessageBox.Show("There are no more posts or service is currently unavailable. Load the previous page?");
            }
        }

        void Refresh()
        {
            currentPageIndex = 1;
            LinksUpdate();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            PostWindow postWindow = new PostWindow();
            postWindow.titleText.Text = "Share a link with us";
            postWindow.theLinksControl.Visibility = Visibility.Visible;
            postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
            postWindow.theStatusPostControl.Visibility = Visibility.Hidden;

            postWindow.ShowDialog();
            string title = postWindow.theLinksControl.Title;
            string body = postWindow.theLinksControl.Body;
            string url = postWindow.theLinksControl.URL;

            MakePost(body, title, url);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Datum data = (Datum)btn.DataContext;

            if (data != null)
            {
                Process process = Process.Start(data.slug);
              
            }
        }


        private async void CommentButton_Click(object sender, RoutedEventArgs e)
        {

            //The logged in user can make a comment by simply clicking this button.
            Button btn = sender as Button;

            Datum datum = (Datum)btn.DataContext;

            if (datum != null)
            {
                commentPoster = new CommentPoster();
                PostWindow postWindow = new PostWindow();
                postWindow.titleText.Text = "Add a comment";
                postWindow.theStatusPostControl.Visibility = Visibility.Visible;
                postWindow.theLinksControl.Visibility = Visibility.Hidden;
                postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
                postWindow.ShowDialog();

                string comment = postWindow.theStatusPostControl.Body;

                string tkBuild = "Bearer " + Token;

                loadingTextBlock.Text = "Making a comment...";

                await commentPoster.MakeAComment(tkBuild, datum.id, comment);

                if (commentPoster.success)
                {
                    loadingTextBlock.Text = "Commented Successfully.";
                    Refresh();

                }
                else
                {

                    loadingTextBlock.Text = "Unable to post your comment. Please try again.";
                    //MessageBox.Show("Unable to comment at this time.");
                }
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {

            string tkBuild = "Bearer " + Token;

            Button editButton = sender as Button;
            Datum datum = (Datum)editButton.DataContext;
            if (datum != null)
            {
                PostWindow postWindow = new PostWindow();

                //Get the post ID.
                int postID = datum.id;
                //Since this is a status post, only the body is needed.
                string body = datum.body;

                postWindow.theStatusPostControl.Visibility = Visibility.Hidden;
                postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
                postWindow.theLinksControl.Visibility = Visibility.Visible;
                //First clear the textbox
                postWindow.theLinksControl.RTBox.Document.Blocks.Clear();
                postWindow.theLinksControl.TitleBox.Clear();
                postWindow.theLinksControl.URLTBox.Document.Blocks.Clear();

                //Append the message to be edited to the rich text box.
                postWindow.theLinksControl.RTBox.AppendText(body);
                postWindow.theLinksControl.TitleBox.Text = datum.title;
                postWindow.theLinksControl.URLTBox.AppendText(datum.url);

                //We are ready for edit.
                postWindow.ShowDialog();
                string returnEditedText = postWindow.theLinksControl.Body;
                string returnedTitle = postWindow.theLinksControl.Title;
                string returnedURL = postWindow.theLinksControl.URL;
                string editEndpoint = @"https://api.africoders.com/v1/edit/post" + "?id=" + postID + "&title=" + returnedTitle +
                    "&body=" + returnEditedText + "&url=" + returnedURL;

                postHelper = new PostHelper(editEndpoint);
                loadingTextBlock.Text = "Editing post...";

                await postHelper.MakePostAsync(tkBuild);
                
                loadingTextBlock.Text = "Post edited successfully..";

                Refresh();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            string tokenBuild = "Bearer " + Token;
            Button btn = sender as Button;

            Datum datum = (Datum)btn.DataContext;
            if (datum != null)
            {
                string postID = datum.id.ToString();

                string deleteEndpoint = @"https://api.africoders.com/v1/del/post?id=" + postID;

                postHelper = new PostHelper(deleteEndpoint);

                loadingTextBlock.Text = "Deleting post...";
                await postHelper.MakePostAsync(tokenBuild);

                loadingTextBlock.Text = "Post Deleted Successfully.";

                Refresh();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
