using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using AfricodersProject.AfricoderModels;
using AfricodersProject.Services;
namespace AfricodersProject.Africoders_Custom_User_Controls
{

    /// <summary>
    /// Interaction logic for StatusFeedControl.xaml
    /// </summary>
    public partial class StatusFeedControl : UserControl
    {

        public int LoggedID { get; set; }
        /// <summary>
        /// Token to be set after login. Value will be passed from the login control.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// The status endpoint to be consumed by the Pagination Helper class.
        /// </summary>
        string endpoint = @"https://api.africoders.com/v1/posts?category=status&include=comment:order(id|asc)&order=published_at|desc&limit=10&page=";

        /// <summary>
        /// The endpoint to make a status post to.
        /// </summary>
        private string statusPostEndpoint = @"https://api.africoders.com/v1/post/status?body=";
        /// <summary>
        /// Keeps tab of the current page index. 
        /// </summary>
        static int currentPageIndex = 1;

        /// <summary>
        /// Obtain important reference to the pagination helper class.
        /// </summary>
        private PaginationHelper paginationHelper;

        /// <summary>
        /// Reference the post helper class.
        /// </summary>
        private PostHelper postHelper;
        /// <summary>
        /// Reference to the comment poster class.
        /// </summary>
        private CommentPoster commentPoster;
        public StatusFeedControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Make a status post.
        /// </summary>
        /// <param name="postBody">Message to be posted to the endpoint.</param>
        private async void MakePost(string postBody)
        {
            string tokenBuilder = "Bearer " + Token;
            statusPostEndpoint = statusPostEndpoint + postBody;

            postHelper = new PostHelper(statusPostEndpoint);

            await postHelper.MakePostAsync(tokenBuilder);
            if (postHelper.StatusPostSuccessful)
            {
                loadingTextBlock.Text = "Successfully Created a Status Post.";
                //reset the board immediately.
                Refresh();
            }
        }
        /// <summary>
        /// Event triggered when the user clicks to see more post.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            Datum data = btn.DataContext as Datum;

            if (data != null)
            {
                try
                {
                    BlogDetailsWindow blogDetailsWindow = new BlogDetailsWindow();

                    blogDetailsWindow.headerTB.Text = "A status post by: " + data.user.name;
                    //Clean the title and body
                    string theTitle = data.title;
                    string theBody = data.body;
                    string cleanedTitle = Regex.Replace(theTitle, @"<[^>]*>", "");
                    string cleanedBody = Regex.Replace(theBody, @"<[^>]*>", "");

                    blogDetailsWindow.titleText.Text = cleanedTitle;
                    blogDetailsWindow.bodyTB.Text = cleanedBody;

                    blogDetailsWindow.userImage.Source = new BitmapImage(new Uri(data.user.avatarUrl));
                    blogDetailsWindow.dateText.Text = data.created.date;
                    blogDetailsWindow.usernameText.Text = data.user.name;

                    foreach (var dat in data.comment.data)
                    {
                        DateTime dateTime = new DateTime();
                        bool dateParse = DateTime.TryParse(dat.created.date, out dateTime);
                        //string date = dat.created.date;
                        if (dateParse)
                        {
                            string convertedTime = Convert.ToDateTime(DateTime.Parse(dateTime.ToString())).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                            dat.created.date = convertedTime;
                        }
                        string comment = Regex.Replace(dat.body, @"<[^>]*>", "");
                        dat.body = comment;
                        //Pass in the ID of the logged in user.
                        dat.LoggedInID = LoggedID;
                    }
                    //Pass the token,important
                    blogDetailsWindow.Token = Token;
                    //Pass the slug
                    blogDetailsWindow.PostSlug = data.slug;

                    blogDetailsWindow.commentsListBox.ItemsSource = data.comment.data;

                    blogDetailsWindow.Show();
                }
                catch(Exception t)
                {
                    MessageBox.Show("A problem occured. Please try again in a few moments. "+t.Message);
                }
            }

        }

        /// <summary>
        /// The paginator that is responsible for efficient page management.
        /// </summary>
        private async void StatusPaginator()
        {
            try
            {
                loadingTextBlock.Text = $"Loading page {currentPageIndex}. A moment please.";
                paginationHelper = new PaginationHelper(endpoint);

                Datum[] data = await paginationHelper.GetNextStatusPosts(currentPageIndex, LoggedID);
                if (data != null)
                {
                    foreach (var bd in data)
                    {
                        //reformat the html adorned body
                        string b = bd.title.Replace("<p>", "")
                                          .Replace("</p>", "")
                                          .Replace("<p>", "")
                                          .Replace("</p>", "")
                                          .Replace("&npsb;", "");

                        bd.body = b;

                        //Find a way to change the date to reflect the current reality.
                        DateTime dateTime = DateTime.Parse(bd.created.date);

                        DateTime currentDate = DateTime.Now;

                        TimeSpan timeSpan = currentDate - dateTime;

                        if (timeSpan.Days <= 1)
                        {
                            bd.created.date = timeSpan.Hours.ToString() + " hours ago";

                        }//Set the date
                        else if (timeSpan.Days > 1)
                        {
                            bd.created.date = timeSpan.Days.ToString() + " days ago";
                        }
                    }

                    //Finally set the data to be seen in view.
                    TheListBox.ItemsSource = data;
                    //MessageBox.Show("Loaded");
                    loadingTextBlock.Text = $"Loaded page {currentPageIndex} successfully.";
                    //Dispose the class instance.
                    paginationHelper.Dispose();
                }
            }
            catch
            {
                MessageBox.Show("There are no more data to load. Sorry. Please load the previous page");

            }
        }
        /// <summary>
        /// Fires when the user clicks to see the next page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click_1(object sender, RoutedEventArgs e)
        {
            //The first page is assumed to be loaded already, using the code behind. Not too neat though.
            currentPageIndex++;// increase the page index.
            StatusPaginator();
        }
        /// <summary>
        /// Fires when the user clicks to see the previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex--;
            StatusPaginator();
        }
        /// <summary>
        /// Triggered when the control becomes visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadingTextBlock.Text = "Africoders Home Page";
        }
        /// <summary>
        /// Handles the making of statuses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            PostWindow postWindow = new PostWindow();


            postWindow.titleText.Text = "What is on your mind right now?";


            //Set the visibility of controls.
            postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
            postWindow.theStatusPostControl.Visibility = Visibility.Visible;
            postWindow.theLinksControl.Visibility = Visibility.Hidden;
            postWindow.ShowDialog();


            string message = postWindow.theStatusPostControl.Body;
            loadingTextBlock.Text = "Creating a status post..";
            //pass in the message.
            MakePost(message);

            //release valuable resources
            postWindow.Close();
        }
        /// <summary>
        /// Refreshes the control.
        /// </summary>
        private void Refresh()
        {
            currentPageIndex = 1;
            StatusPaginator();
        }
        /// <summary>
        /// Refreshes the page, on the command of the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
        /// <summary>
        /// Fires when the user clicks on Add a Comment, on the status page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                string tkBuild = "Bearer " + Token;
                string comment = postWindow.theStatusPostControl.Body;

                loadingTextBlock.Text = "Making a comment...";
                await commentPoster.MakeAComment(tkBuild, datum.id, comment);

                if (commentPoster.success == true)
                {
                    loadingTextBlock.Text = "Commented Successfully.";
                    Refresh();
                }
                else
                    loadingTextBlock.Text = "Unable to post your comment at this time. Please try again later.";
                    //MessageBox.Show("Unable to comment at this time.");
            }
        }
        /// <summary>
        /// Triggered when the user is interested in editing a post.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                postWindow.theStatusPostControl.Visibility = Visibility.Visible;
                postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
                postWindow.theLinksControl.Visibility = Visibility.Hidden;
                //First clear the textbox
                postWindow.theStatusPostControl.RTBox.Document.Blocks.Clear();
                //Append the message to be edited to the rich text box.
                postWindow.theStatusPostControl.RTBox.AppendText(body);
                //We are ready for edit.
                postWindow.ShowDialog();
                string returnEditedText = postWindow.theStatusPostControl.Body;
                string editEndpoint = @"https://api.africoders.com/v1/edit/post" + "?id=" + postID + "&body=" + returnEditedText;

                postHelper = new PostHelper(editEndpoint);
                loadingTextBlock.Text = "Editing post...";

                await postHelper.MakePostAsync(tkBuild);

                //MessageBox.Show("Post edited successfully.");

                loadingTextBlock.Text = "Post edited.";

                //Refresh
                Refresh();
            }
        }
        /// <summary>
        /// Fires when the user intends to deletes own post.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}
