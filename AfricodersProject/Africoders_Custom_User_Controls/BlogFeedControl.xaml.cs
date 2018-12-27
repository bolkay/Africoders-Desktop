using AfricodersProject.AfricodersBlogModel;
using AfricodersProject.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for BlogFeedControl.xaml
    /// The logic of this control is essentially similar to that of status and other important categories.
    /// However,it is better to write separate helper functions for each individual target category.
    /// </summary>
    public partial class BlogFeedControl : UserControl
    {
        public int LoggedID { get; set; }
        /// <summary>
        /// Reference to the comment poster.
        /// </summary>
        private CommentPoster commentPoster;
        /// <summary>
        /// Gets the token for the logged in user.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Important! Endpoint of the target API for blog posts and attendant comments.
        /// </summary>
        private string endpoint= @"https://api.africoders.com/v1/posts?category=blog&include=comment:order(id|asc)&order=published_at|desc&limit=10&page=";

        private string BlogPoseEndpoint = @"https://api.africoders.com/v1/post/blog?";

        /// <summary>
        /// Obtain good reference to the PostHelper class.
        /// </summary>
        private PostHelper postHelper;
        /// <summary>
        /// Keeps tab of individual pages.
        /// </summary>
        private int currentPageIndex = 1;
        /// <summary>
        /// Obtains a direct reference to the pagination helper functions class.
        /// </summary>
        private PaginationHelper paginationHelper;

        public BlogFeedControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Blog post helper method.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        private async void MakeBlogPost(string title,string body)
        {
            string tkBuilder = "Bearer " + Token;
            BlogPoseEndpoint = BlogPoseEndpoint + "title=" + title + "&body=" + body;
            postHelper = new PostHelper(BlogPoseEndpoint);
            await postHelper.MakePostAsync(tkBuilder);
            if (postHelper.StatusPostSuccessful)
            {
                loadingTextBlock.Text = "Blog Post Made Successfully.";
                //Refresh the control
                Refresh();
                //MessageBox.Show("Posted Successfully.");
            }
            else
            {
                loadingTextBlock.Text = "Unable to make a post at this time. Please try again.";
                //MessageBox.Show("Unable to make a post at this time.");
            }

        }


        /// <summary>
        /// Button for transferring data to the necessary window for individual blog posts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //testing xaml reader and writer functions
            
            Button button = sender as Button;

            Datum data = (Datum)button.DataContext;

            if (data != null)
            {

                try
                {
                    //Using the forum window instead.
                    //I love the layout more.
                    BlogDetailsWindow blogDetailsWindow = new BlogDetailsWindow();

                    blogDetailsWindow.headerTB.Text = "A blog post by: " + data.user.name;
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
                        string comment = Regex.Replace(dat.body, @"<[^>]*>", "");
                        dat.body = comment;
                        DateTime dateTime = new DateTime();
                        bool dateParse = DateTime.TryParse(dat.created.date, out dateTime);
                        //string date = dat.created.date;
                        if (dateParse)
                        {
                            string convertedTime = Convert.ToDateTime(DateTime.Parse(dateTime.ToString())).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                            dat.created.date = convertedTime;
                        }
                        //Pass in the ID of the logged in user.
                        dat.LoggedInID = LoggedID;
                    }
                    //Pass the slug
                    blogDetailsWindow.PostSlug = data.slug;
                    //pass the token
                    blogDetailsWindow.Token = Token;

                    blogDetailsWindow.commentsListBox.ItemsSource = data.comment.data;

                    blogDetailsWindow.Show();
                }
                catch (Exception b) { MessageBox.Show(b.Message); }
            }
        }

        /// <summary>
        /// Updates the blog post to reflect the current index.
        /// </summary>
        private async void BlogUpdate()
        {
            try
            {
                loadingTextBlock.Text = $"Loading page {currentPageIndex}. Hold on a moment please.";
                paginationHelper = new PaginationHelper(endpoint);

                Datum[] data = await paginationHelper.GetNextBlogPosts(currentPageIndex,LoggedID);

                if (data != null)
                {
                    foreach (var result in data)
                    {
                        string date = result.created.date;
                        
                        string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                        result.created.date = convertedTime;
                    }
                    //Finally, set the data
                    TheLB.ItemsSource = data;
                    //MessageBox.Show("loaded");
                    loadingTextBlock.Text = $"Finished loading page {currentPageIndex}";
                    //dispose the helper
                    paginationHelper.Dispose();
                }

            }
            catch
            {
                loadingTextBlock.Text= "Unable to load the page. Consider loading the previous page";
                //MessageBox.Show("Unable to load the page. Consider loading the previous page");

            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex++;
            BlogUpdate();
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex--;
            BlogUpdate();
        }
        /// <summary>
        /// Fires when the control loads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadingTextBlock.Text = "Africoders Blog Posts";
        }
        /// <summary>
        /// Refreshes the control.
        /// </summary>

        private void Refresh()
        {
            currentPageIndex = 1;
            BlogUpdate();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Handles the creation of blog posts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PostWindow postWin = new PostWindow();
                postWin.theBlogPostControl.Visibility = Visibility.Visible;
                postWin.theStatusPostControl.Visibility = Visibility.Hidden;
                postWin.theLinksControl.Visibility = Visibility.Hidden;
                postWin.titleText.Text = "Share a blog post!";

                postWin.ShowDialog();

                string title = postWin.theBlogPostControl.PostTitle;
                string body = postWin.theBlogPostControl.PostBody;

                MakeBlogPost(title, body);
                
            }

            catch
            {
                MessageBox.Show("Unable to complete the operation at this time.");
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

                }
                else
                {

                    loadingTextBlock.Text = "Unable to post your comment.";
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
                string title = datum.title;
                postWindow.theStatusPostControl.Visibility = Visibility.Hidden;
                postWindow.theBlogPostControl.Visibility = Visibility.Visible;
                postWindow.theLinksControl.Visibility = Visibility.Hidden;

                //First clear the textbox
                postWindow.theStatusPostControl.RTBox.Document.Blocks.Clear();
                postWindow.theBlogPostControl.TitleBox.Clear();
                //Append the message to be edited to the rich text box.

                postWindow.theBlogPostControl.RTBox.AppendText(body);
                postWindow.theBlogPostControl.TitleBox.Text = title;
                //We are ready for edit.
                postWindow.ShowDialog();

                string returnEditedText = postWindow.theBlogPostControl.PostBody;
                string EditedTitle = postWindow.theBlogPostControl.PostTitle;

                string editEndpoint = @"https://api.africoders.com/v1/edit/post" + "?id=" + postID +"&title="+EditedTitle+ "&body=" + returnEditedText;

                postHelper = new PostHelper(editEndpoint);
                loadingTextBlock.Text = "Editing post...";

                await postHelper.MakePostAsync(tkBuild);

                //MessageBox.Show("Post edited successfully.");

                loadingTextBlock.Text = "Post edited.";

                //Refresh
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
    }
}
