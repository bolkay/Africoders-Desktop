using AfricodersProject.AfricoderJobModel;
using AfricodersProject.Services;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static AfricodersProject.AfricoderJobModel.AfricoderJobsFeed;

namespace AfricodersProject.Africoders_Custom_User_Controls
{

    /// <summary>
    /// Interaction logic for JobsControl.xaml
    /// </summary>
    public partial class JobsControl : UserControl
    {
        public int LoggedId { get; set; }

        /// <summary>
        /// Token to be set after login. Value will be passed from the login control.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// The status endpoint to be consumed by the Pagination Helper class.
        /// </summary>
        string endpoint = @"https://api.africoders.com/v1/posts?category=job&include=comment:order(id|asc)&order=published_at&limit=10&page=";

        /// <summary>
        /// The endpoint to make a status post to.
        /// </summary>
        private string jobPostEndpoint = @"https://api.africoders.com/v1/post/job";
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
        /// Reference to the comment poster.
        /// </summary>
        private CommentPoster commentPoster;
        /// <summary>
        /// Make a status post.
        /// </summary>
        /// <param name="postBody">Message to be posted to the endpoint.</param>
        private async void MakePost(string postBody,string postTitle)
        {
            string tokenBuilder = "Bearer " + Token;
            jobPostEndpoint = jobPostEndpoint +"?title="+postTitle+"&body="+postBody;

            postHelper = new PostHelper(jobPostEndpoint);

            await postHelper.MakePostAsync(tokenBuilder);

            if (postHelper.StatusPostSuccessful==true)
            {
                loadingTextBlock.Text = "Job Opening Posted Successfully.";
                //MessageBox.Show("Posted.");
                //Refresh the control.
                Refresh();
            }
            else
            {
                loadingTextBlock.Text = "Unable to post your job opening. Please try again.";
                //MessageBox.Show("Unsucessful Operation.");
            }
        }

        public JobsControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            loadingTextBlock.Text = "Africoders Jobs";
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex++;
            JobsUpdate();
        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageIndex--;
            JobsUpdate();
        }
        private async void JobsUpdate()
        {
            try
            {
                loadingTextBlock.Text = $"Loading Job page {currentPageIndex}. Hold on a moment please.";
                paginationHelper = new PaginationHelper(endpoint);

                //loadingTextBlock.Text = $"Loading page {currentPageIndex}";
                Datum[] data = await paginationHelper.GetNextJobsPosts(currentPageIndex,LoggedId);

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
                else
                {
                    MessageBox.Show("Unable to load the page. Consider loading the previous page");
                }
            }
            catch
            {
                MessageBox.Show("There are no more posts. Load the previous page?");
            }
        }

        void Refresh()
        {
            currentPageIndex = 1;
            JobsUpdate();
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            PostWindow postWindow = new PostWindow();
            postWindow.titleText.Text = "Submit a job Advert.";
            postWindow.theBlogPostControl.Visibility = Visibility.Visible;
            postWindow.theStatusPostControl.Visibility = Visibility.Hidden;
            postWindow.theLinksControl.Visibility = Visibility.Hidden;

            postWindow.ShowDialog();
            //Since the blog post and job advert post are similar, they can effectively sgare the same control.
            string title = postWindow.theBlogPostControl.PostTitle;
            string body = postWindow.theBlogPostControl.PostBody;

            MakePost(body, title);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            AfricoderJobsFeed.Datum data = btn.DataContext as AfricoderJobsFeed.Datum;
            //Load up the category window.
            CategoryDetailsWindow categoryDetailsWindow = new CategoryDetailsWindow();
            if (data != null)
            {
                BlogDetailsWindow blogDetailsWindow = new BlogDetailsWindow();

                blogDetailsWindow.headerTB.Text = "A job post by: " + data.user.name;
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
                    dat.LoggedInID = LoggedId;
                }
                //Pass the slug
                blogDetailsWindow.PostSlug = data.slug;
                //pass the token
                blogDetailsWindow.Token = Token;
                blogDetailsWindow.commentsListBox.ItemsSource = data.comment.data;

                blogDetailsWindow.Show();
            }
            
        }

        private async void CommentButton_Click(object sender, RoutedEventArgs e)
        {
            //The logged in user can make a comment by simply clicking this button.
            Button btn = sender as Button;

           AfricoderJobsFeed.Datum datum = (AfricoderJobsFeed.Datum)btn.DataContext;

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
                    loadingTextBlock.Text = "Unable to comment at this time.";
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
                postWindow.theBlogPostControl.TitleBox.Clear();
                postWindow.theBlogPostControl.RTBox.Document.Blocks.Clear();


                //Append the message to be edited to the rich text box.
                postWindow.theBlogPostControl.TitleBox.Text = title;
                postWindow.theBlogPostControl.RTBox.AppendText(body);
                //We are ready for edit.
                postWindow.ShowDialog();
                string returnEditedText = postWindow.theStatusPostControl.Body;
                string editEndpoint = @"https://api.africoders.com/v1/edit/post" + "?id=" + postID.ToString() +"&title="+title+ "&body=" + returnEditedText;

                postHelper = new PostHelper(editEndpoint);
                loadingTextBlock.Text = "Editing post...";

                await postHelper.MakePostAsync(tkBuild);

                
                loadingTextBlock.Text = "Post Succssfully edited.";

                //refresh
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
