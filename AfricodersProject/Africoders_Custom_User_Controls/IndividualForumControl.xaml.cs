using AfricodersProject.ForumModel;
using AfricodersProject.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for IndividualForum.xaml
    /// </summary>
    public partial class IndividualForumControl : UserControl
    {
        public string Token { get; set; }
        /// <summary>
        /// Stores the ID of the logged in User
        /// </summary>
        public int LoggedID { get; set; }

        /// <summary>
        /// Get the forum ID.
        /// </summary>
        public string FID { get; set; }

        public string Path { get; set; }
        /// <summary>
        /// Indexing for fluent dynamic pagination.
        /// </summary>
        private int currentPageIndex = 1;

        /// <summary>
        /// Reference to the pagination helper.
        /// </summary>
        private PaginationHelper paginationHelper;
        /// <summary>
        /// Reference to the comment poster.
        /// </summary>
        private CommentPoster commentPoster;
        /// <summary>
        /// Reference to the post helper.
        /// </summary>
        private PostHelper postHelper;

        public IndividualForumControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles the making of forum posts. Only authorised user can make posts.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="fid"></param>
        private async void MakeAForumPost(string title, string body, string fid)
        {
            string tkBuild = "Bearer " + Token;
            string endPoint = @"https://api.africoders.com/v1/post/forum?title=" + title + "&body=" + body + "&fid=" + fid;

            postHelper = new PostHelper(endPoint);

            loadingTextBlock.Text = "Making a forum post";
            await postHelper.MakePostAsync(tkBuild);

            if (postHelper.StatusPostSuccessful)
            {
                loadingTextBlock.Text = "Posted Successfully";
                //Refresh the page
                Refresh();
            }
            else
            {
                loadingTextBlock.Text = "Unable to create post at this time. Please try again.";
            }
        }
        /// <summary>
        /// A form of paginator for the board page.
        /// </summary>
        private async void ForumUpdate()
        {

            string endPoint = @"https://api.africoders.com/v1/" + Path + "?page=" + currentPageIndex + "&order=updated_at|DESC&include=comment";
            try
            {
                loadingTextBlock.Text = $"Loading page {currentPageIndex}. Hold on a moment please.";
                paginationHelper = new PaginationHelper(endPoint);

                List<ForumModel.Datum> data = await paginationHelper.GetNextForumPosts(currentPageIndex, LoggedID);

                //jsonObtainer = new JsonObtainer(endPoint, "Forum_Agent_Bolkay");
                //string json = await jsonObtainer.GetJsonStringAsync();

                //IndividualForum individualForum = Newtonsoft.Json.JsonConvert.DeserializeObject<IndividualForum>(json);


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
                    loadingTextBlock.Text = $"Finished loading page {currentPageIndex}";
                    //dispose the helper
                    paginationHelper.Dispose();
                }

            }
            catch
            {
                MessageBox.Show("Unable to load the page. Consider loading the previous page");

            }
        }
        /// <summary>
        /// Fires when the user intends to make a comment.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CommentButton_Click(object sender, RoutedEventArgs e)
        {

            string tkBuild = "Bearer " + Token;

            Button btn = sender as Button;
            ForumModel.Datum datum = (ForumModel.Datum)btn.DataContext;

            if (datum != null)
            {
                int Id = datum.id;
                commentPoster = new CommentPoster();
                PostWindow postWindow = new PostWindow();

                postWindow.theStatusPostControl.Visibility = Visibility.Visible;
                postWindow.theLinksControl.Visibility = Visibility.Hidden;
                postWindow.theBlogPostControl.Visibility = Visibility.Hidden;
                postWindow.ShowDialog();

                string comment = postWindow.theStatusPostControl.Body;


                loadingTextBlock.Text = "Making a comment...";

                await commentPoster.MakeAComment(tkBuild, Id, comment);

                if (commentPoster.success)
                {
                    loadingTextBlock.Text = "Commented Successfully.";
                    Refresh();
                }
                else
                {

                    loadingTextBlock.Text = "Unable to post your comment.";
                    //MessageBox.Show("Unable to comment at this time.");
                }
            }
        }
        /// <summary>
        /// Handles the update of user posts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            PostWindow postWindow = new PostWindow();
            string tokenBuild = "Bearer " + Token;
            Button btn = sender as Button;

            ForumModel.Datum datum = (ForumModel.Datum)btn.DataContext;

            if (datum != null)
            {
                postWindow.theBlogPostControl.Visibility = Visibility.Visible;
                postWindow.theStatusPostControl.Visibility = Visibility.Hidden;
                postWindow.theLinksControl.Visibility = Visibility.Hidden;


                postWindow.titleText.Text = "Edit your forum post";
                //Reformat the titleBox to reflect the current text.
                postWindow.theBlogPostControl.TitleBox.Text = datum.title;

                string bodyConverted = datum.body;
                //Clear the TB
                postWindow.theBlogPostControl.RTBox.Document.Blocks.Clear();

                postWindow.theBlogPostControl.RTBox.AppendText(bodyConverted);

                postWindow.ShowDialog();
                string editedTitle = postWindow.theBlogPostControl.PostTitle;
                string editedBody = postWindow.theBlogPostControl.PostBody;

                int id = datum.id;
                string endpoint = @"https://api.africoders.com/v1/edit/post?id=" + id.ToString() + "&title=" + editedTitle + "&body=" + editedBody;

                postHelper = new PostHelper(endpoint);
                loadingTextBlock.Text = "Editing Post.";
                await postHelper.MakePostAsync(tokenBuild);
                if (postHelper.StatusPostSuccessful)
                {
                    loadingTextBlock.Text = "Edited Post Successfully.";
                    Refresh();
                }
                else
                {
                    loadingTextBlock.Text = "Unable to edit post. Please try again.";
                }
            }

        }
        /// <summary>
        /// Refreshes the control.
        /// </summary>
        private void Refresh()
        {
            currentPageIndex = 1;
            ForumUpdate();
        }
        /// <summary>
        /// Fires when the user intends to delete his or her post.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string tokenBuild = "Bearer " + Token;
            Button btn = sender as Button;

            ForumModel.Datum datum = (ForumModel.Datum)btn.DataContext;

            if (datum != null)
            {

                int id = datum.id;

                string endpoint = @"https://api.africoders.com/v1/del/post?id=" + id.ToString();

                postHelper = new PostHelper(endpoint);
                loadingTextBlock.Text = "Deleting Post.";
                await postHelper.MakePostAsync(tokenBuild);
                if (postHelper.StatusPostSuccessful)
                {
                    loadingTextBlock.Text = "Deleted Post Successfully.";
                    Refresh();
                }
                else
                {
                    loadingTextBlock.Text = "Unable to delete post. Please try again.";
                }
            }
        }
        /// <summary>
        /// Triggered when the user intends to read more on a specific topic.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            ForumModel.Datum datum = (ForumModel.Datum)btn.DataContext;


            if (datum != null)
            {
                ForumWindow forumWindow = new ForumWindow();
                string bodyText = datum.body.ToString();
                string TitleText = datum.title.ToString();
                string dateText = datum.created.date.ToString();
                string userName = datum.user.name.ToString();
                string AvaterUrl = datum.user.avatarUrl.ToString();

                forumWindow.headerTB.Text = "A forum post by: " + userName;
                forumWindow.bodyTB.Text = bodyText;
                forumWindow.titleText.Text = TitleText;
                forumWindow.dateText.Text = dateText;
                forumWindow.usernameText.Text = userName;
                forumWindow.userImage.Source = new BitmapImage(new Uri(AvaterUrl));
                //Reformat the body of comments to remove html tags

                foreach (var m in datum.comment.data)
                {
                    string theBody = m.body;

                    string reformatted = Regex.Replace(theBody, @"<[^>]*>", "");
                    DateTime dateTime = new DateTime();
                    bool dateParse = DateTime.TryParse(m.created.date, out dateTime);
                    //string date = dat.created.date;
                    if (dateParse)
                    {
                        string convertedTime = Convert.ToDateTime(DateTime.Parse(dateTime.ToString())).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                        m.created.date = convertedTime;
                    }
                    m.body = reformatted.Replace("<p>", "").Replace("<b>", "").Replace("</b>", "").Replace("</p>", "");

                    m.LoggedInID = LoggedID;
                }
                //Pass slug to the forum
                forumWindow.PostSlug = datum.slug;
                //Pass the token from this page.
                forumWindow.Token = Token;
                forumWindow.commentsListBox.ItemsSource = datum.comment.data;
                forumWindow.Show();
            }
        }

        /// <summary>
        /// Fires when the control starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadingTextBlock.Text = "Africoder Forums";
        }
        /// <summary>
        /// Next page handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex++;
            ForumUpdate();
        }
        /// <summary>
        /// Previous page handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex--;
            ForumUpdate();
        }
        /// <summary>
        /// Refresh button handler. Refreshes the forum page to 1.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Handles the creation of forum posts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {

            //Access the post window
            PostWindow postWindow = new PostWindow();
            //We can use the blog control for forums
            postWindow.theBlogPostControl.Visibility = Visibility.Visible;
            postWindow.theLinksControl.Visibility = Visibility.Hidden;
            postWindow.theStatusPostControl.Visibility = Visibility.Hidden;

            try
            {
                postWindow.theBlogPostControl.TitleBox.Text = "Enter your forum post title.";

                postWindow.ShowDialog();
                //Get the title of the post.
                string title = postWindow.theBlogPostControl.PostTitle;
                string body = postWindow.theBlogPostControl.PostBody;

                MakeAForumPost(title, body, FID);
            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);
            }
        }
    }
}
