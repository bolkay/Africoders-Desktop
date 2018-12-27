using AfricodersProject.AfricoderJobModel;
using AfricodersProject.AfricoderLinksModel;
using AfricodersProject.AfricodersBlogModel;
using AfricodersProject.ForumModel;
using AfricodersProject.Services;
using AfricodersProject.UIManagement;
using AfricodersProject.ViewManagement;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AfricodersProject
{
    /// <summary>
    /// Interaction logic for AfricodersPage.xaml
    /// </summary>
    public partial class AfricodersPage : Window
    {
        /// <summary>
        /// Will receive the established reference of this window from the login Page.
        /// </summary>
        public MainWindow mainWindow;
        /// <summary>
        /// This is the important ID of the logged in user, obatined from the initial distribution from the login class.
        /// This is important for any user specific actions.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Reference to the forum management class.
        /// </summary>
        private ForumManagement forumManagement;

        /// <summary>
        /// Reference to the internet checking service.
        /// </summary>
        private InternetChecker InternetChecker = new InternetChecker();
        /// <summary>
        /// It is very important to check for internet connection before doing anything at all.
        /// </summary>
        private bool internetConnection = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Reference to the Blog Management class for the Africoders Page.
        /// </summary>
        private BlogManagement blogManagement;
        /// <summary>
        /// Endpoint to fetch the list of forums.
        /// </summary>
        private string forumEndpoint = @"https://api.africoders.com/v1/forums";
        /// <summary>
        /// Obtain reference to the Blog endpoint of the Afriocders API.
        /// </summary>
        string BlogEndpoint = @"https://api.africoders.com/v1/posts?category=blog&include=comment:order(id|asc)&order=published_at|desc&limit=10";

        /// <summary>
        /// Check if we have loaded the blog posts for the first time.
        /// </summary>
        private bool BlogLoaded = false;

        /// <summary>
        /// Check if we have loaded the status feed for the first time.
        /// </summary>
        private bool StatusFeedLoaded = false;
        /// <summary>
        /// Obtain the status endpoint of the Africoders API.
        /// </summary>
        string StatusEndpoint = @"https://api.africoders.com/v1/posts?category=status&include=comment:order(id|asc)&order=published_at|desc&limit=10&page=";
        /// <summary>
        /// Get the jobs endpoint of the Africoders API.
        /// </summary>
        private string JobEndpoint = @"https://api.africoders.com/v1/posts?category=job&include=comment:order(id|asc)&order=id|asc&limit=10&page=";
        /// <summary>
        /// Have we loaded the job postings?
        /// </summary>
        private bool JobsLoaded = false;

        /// <summary>
        /// Have we loaded the forum yet?
        /// </summary>
        private bool ForumLoaded = false;

        /// <summary>
        /// A reference to the Information Loader for the status page only.
        /// </summary>
        InfoLoader _infoLoader;

        /// <summary>
        /// A messgae to be displayed in case of network connection error.
        /// Also fired when the user is not connected to the internet.
        /// </summary>
        private const string NetworkFailedMessage = "A network error has occured.";
        /// <summary>
        /// Gets the links endpoint.
        /// </summary>
        private string LinksEndpoint = @"https://api.africoders.com/v1/posts?category=link&order=id|asc&limit=10";
        /// <summary>
        /// Have we loaded the link postings?
        /// </summary>
        private bool LinksLoaded = false;

        /// <summary>
        /// Handles the pagination. Note that pagination is more correctly handled at individual control levels.
        /// </summary>
        static int currentPageIndex = 1;
       
        /// <summary>
        /// A simple reference to the JsonObtainer class for sending GET requests.
        /// </summary>
        private JsonObtainer jsonObtainer;

        /// <summary>
        /// The window default constructor.
        /// </summary>
        public AfricodersPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// A cool way to let the user know which section is currently being viewed.
        /// </summary>
        /// <param name="button"></param>
        private void SetThickness(Button button)
        {
            //A way of randomising the navigation colors.
            SolidColorBrush[] solidColorBrushes = new SolidColorBrush[]
            {
                Brushes.Chocolate,Brushes.DarkGreen,Brushes.DarkOrange
            };

            selectedIndexGrid.Background = solidColorBrushes[new Random().Next(solidColorBrushes.Length)];

            selectedIndexGrid.Visibility = Visibility.Visible;
            selectedIndexGrid.Margin = new Thickness(236, button.Margin.Top, 0, 0);
        }
        /// <summary>
        /// Enables the user to directly send mail to the administrator of Africoders.com.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contactButton_Click(object sender, RoutedEventArgs e)
        {
            UIManager.Instance.ViewContactPage();
        }
        /// <summary>
        /// Makes the application draggable. Enables the user to drag the window around on the desktop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        /// <summary>
        /// Fires when the user decides to close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            ManageCloseLogOut("Do you want to Close Africoders?", "Close Africoders?");
        }
        /// <summary>
        /// Displays the Blogs page and hides other sections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BlogButton_Click(object sender, RoutedEventArgs e)
        {
            SetThickness(BlogsButton);
            theProfilePage.Visibility = Visibility.Hidden;
            temporaryBlogGrid.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;
            WelcomeControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            helpControl.Visibility = Visibility.Hidden;
            theLinksControl.Visibility = Visibility.Hidden;
            theJobsControl.Visibility = Visibility.Hidden;
            currentPageTextBlock.Text = "BLOG POSTS";
            forumControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;

            if (!BlogLoaded)
            {
                temporaryBlogGrid.Visibility = Visibility.Visible;

                FetchText.Text = "Fetching blog posts for you...";
                try
                {
                    if (InternetChecker.InternetAvailable())
                    {
                        jsonObtainer = new JsonObtainer(BlogEndpoint, "From_BOLKAY_BLOG_SURF");
                        string jsonResult = await jsonObtainer.GetJsonStringAsync();

                        AfricoderBlogFeed africoderBlogFeed = JsonConvert.DeserializeObject<AfricoderBlogFeed>(jsonResult);

                        if (jsonResult != string.Empty)
                        {
                            foreach (var result in africoderBlogFeed.data)
                            {
                                string date = result.created.date;


                                string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                                result.created.date = convertedTime;
                            }

                            foreach (var dat in africoderBlogFeed.data)
                            {
                                dat.LoggedInID = ID;
                            }

                            blogControl.TheLB.ItemsSource = africoderBlogFeed.data;
                        }
                        //display the blog
                        blogControl.Visibility = Visibility.Visible;

                        temporaryBlogGrid.Visibility = Visibility.Hidden;

                        //MessageBox.Show("Done");

                        //Blog has been loaded
                        BlogLoaded = true;
                    }
                    else
                    {
                        FetchText.Text = NetworkFailedMessage;
                    }
                }
                catch
                {
                    string errorResult = await jsonObtainer.GetJsonStringAsync();
                    FetchText.Text = NetworkFailedMessage;
                }
            }
            else
            {
                blogControl.Visibility = Visibility.Visible;
                //MessageBox.Show("Blog has been loaded");
            }
        }
        /// <summary>
        /// Displays the status page and hides other sections.
        /// This is similar to the page a user sees upon logging into Africoders.com.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            SetThickness(HomeButton);
            theProfilePage.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;

            theJobsControl.Visibility = Visibility.Hidden;

            blogControl.Visibility = Visibility.Hidden;

            theLinksControl.Visibility = Visibility.Hidden;

            WelcomeControl.Visibility = Visibility.Hidden;

            statusControl.Visibility = Visibility.Hidden;

            forumControl.Visibility = Visibility.Hidden;

            helpControl.Visibility = Visibility.Hidden;

            theIndividualForumControl.Visibility = Visibility.Hidden;
            currentPageTextBlock.Text = "HOME";

            _infoLoader = new InfoLoader();

            if (!StatusFeedLoaded)
            {
                temporaryBlogGrid.Visibility = Visibility.Visible;

                FetchText.Text = "Fetching the Home Feed for You...";

                try
                {
                    if (InternetChecker.InternetAvailable())
                    {
                        
                        statusControl.TheListBox.ItemsSource = await _infoLoader.GetStuffDone(StatusEndpoint, currentPageIndex, ID);
                        statusControl.Visibility = Visibility.Visible;

                        temporaryBlogGrid.Visibility = Visibility.Hidden;

                        //we have loaded the status
                        StatusFeedLoaded = true;
                    }
                    else if(!internetConnection)
                    {
                        FetchText.Text = NetworkFailedMessage;
                    }
                }
                catch
                {
                    FetchText.Text = "A connection error has occured. Please try again.";
                    //MessageBox.Show("A connection error has occured. Please try again.");
                }
            }
            else
            {
                statusControl.Visibility = Visibility.Visible;
                //MessageBox.Show("Already loaded feed");
            }

        }
        /// <summary>
        /// Displays the Chats page and hides other sections.
        /// The chats doesn't exist yet. As can be seen on the main site too.
        /// Feature will be added when the main site is ready.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatsButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageTextBlock.Text = "CHATS";
            SetThickness(ChatsButton);
        }
        /// <summary>
        /// Display a list of boards in the forum section. This section has been developed by using custom mapping functions.
        /// No list is provided by default from the endpoint, so it was necessary to develop a custom mapping class to handle this case.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ForumButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageTextBlock.Text = "FORUMS";
            SetThickness(ForumButton);
            forumControl.Visibility = Visibility.Hidden;

            theProfilePage.Visibility = Visibility.Hidden;
            temporaryBlogGrid.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;
            WelcomeControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            theLinksControl.Visibility = Visibility.Hidden;
            theJobsControl.Visibility = Visibility.Hidden;
            helpControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;

            if (!ForumLoaded)
            {

                forumControl.Visibility = Visibility.Hidden;
                temporaryBlogGrid.Visibility = Visibility.Visible;

                FetchText.Text = "Getting Forums for you.";

                try
                {
                    if (InternetChecker.InternetAvailable())
                    {
                        jsonObtainer = new JsonObtainer(forumEndpoint, "Forum Agent From Bolkay");

                        //  await forumManagement.ForumBoardManagement(jsonObtainer, forumControl);
                        //Get the json
                        string json = await jsonObtainer.GetJsonStringAsync();

                        //map the obtained json to forum feed.
                        ForumFeed forumFeed = Newtonsoft.Json.JsonConvert.DeserializeObject<ForumFeed>(json);

                        //use the mapper class to get the general category
                        ForumMapper forumMapper = new ForumMapper();

                        //Get the list of general categories from the forum feed.
                        var generalCategories = forumMapper.GeneralCategories(forumFeed);
                        //Get the list of web design categories
                        var webCategories = forumMapper.WebCategories(forumFeed);
                        //Get the mobile categories
                        var mobileCategories = forumMapper.MobileCategories(forumFeed);
                        //Finally set the datasource
                        //Load the app categories
                        var appCategories = forumMapper.AppCategories(forumFeed);

                        //Get the database categories
                        var databaseCategories = forumMapper.DatabaseCategories(forumFeed);

                        //get the commmunity categories
                        var communityCategories = forumMapper.CommunityCategories(forumFeed);
                        foreach (var dat in webCategories)
                        {
                            string text = dat.latest;

                            string formatted = Regex.Replace(text, @"<[^>]*>", "");
                            dat.latest = formatted;
                        }
                        foreach (var dat in generalCategories)
                        {
                            string text = dat.latest;

                            string formatted = Regex.Replace(text, @"<[^>]*>", "");
                            dat.latest = formatted;
                        }
                        foreach (var dat in mobileCategories)
                        {
                            string text = dat.latest;

                            string formatted = Regex.Replace(text, @"<[^>]*>", "");
                            dat.latest = formatted;
                        }
                        foreach (var dat in appCategories)
                        {
                            string text = dat.latest;

                            string formatted = Regex.Replace(text, @"<[^>]*>", "");
                            dat.latest = formatted;
                        }

                        foreach (var dat in databaseCategories)
                        {
                            string text = dat.latest;

                            string formatted = Regex.Replace(text, @"<[^>]*>", "");
                            dat.latest = formatted;
                        }
                        foreach (var dat in communityCategories)
                        {
                            string text = dat.latest;

                            string formatted = Regex.Replace(text, @"<[^>]*>", "");
                            dat.latest = formatted;
                        }
                        forumControl.TheGeneralForumListBox.ItemsSource = generalCategories;

                        forumControl.WebGeneralForumListBox.ItemsSource = webCategories;

                        forumControl.MobileGeneralForumListBox.ItemsSource = mobileCategories;

                        forumControl.AppGeneralForumListBox.ItemsSource = appCategories;

                        forumControl.DatabaseGeneralForumListBox.ItemsSource = databaseCategories;

                        forumControl.CommunityGeneralForumListBox.ItemsSource = communityCategories;


                        temporaryBlogGrid.Visibility = Visibility.Hidden;

                        forumControl.Visibility = Visibility.Visible;

                        ForumLoaded = true;
                    }
                    else
                    {
                        FetchText.Text = NetworkFailedMessage;
                    }
                }
                catch
                {
                    string error = await jsonObtainer.GetJsonStringAsync();
                    FetchText.Text = error;
                }
            }
            else
            {
                //Display the forum
                forumControl.Visibility = Visibility.Visible;

                //MessageBox.Show("Forum has been loaded.");
            }
        }
        /// <summary>
        /// Displays the Jobs page and hides other sections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void JobsButton_Click(object sender, RoutedEventArgs e)
        {
            SetThickness(JobsButton);
            //Make the jobs control visible.
            theJobsControl.Visibility = Visibility.Hidden;
            theProfilePage.Visibility = Visibility.Hidden;
            WelcomeControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;
            theLinksControl.Visibility = Visibility.Hidden;
            forumControl.Visibility = Visibility.Hidden;
            helpControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;
            
            currentPageTextBlock.Text = "JOBS";

            if (!JobsLoaded)
            {
                //Set the temporary blog gid visible
                temporaryBlogGrid.Visibility = Visibility.Visible;
                FetchText.Text = "Fetching available jobs for you...";
                try
                {
                    if (InternetChecker.InternetAvailable())
                    {
                        jsonObtainer = new JsonObtainer(JobEndpoint, "The-Jobs-Finder-From-Bolkay");

                        string jsonResult = await jsonObtainer.GetJsonStringAsync();

                        AfricoderJobsFeed africoderJobsFeed = JsonConvert.DeserializeObject<AfricoderJobsFeed>(jsonResult);

                        //Set the ID of the logged user.
                        foreach (var dat in africoderJobsFeed.data)
                        {
                            string date = dat.created.date;
                            string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                            dat.created.date = convertedTime;
                            dat.LoggedID = ID;
                        }
                        //Set the data source accordingly.
                        theJobsControl.TheListBox.ItemsSource = africoderJobsFeed.data;
                        theJobsControl.Visibility = Visibility.Visible;
                        temporaryBlogGrid.Visibility = Visibility.Hidden;


                        JobsLoaded = true;
                    }
                    else
                    {
                        FetchText.Text = NetworkFailedMessage;
                    }
                }
                catch
                {
                    string result = await jsonObtainer.GetJsonStringAsync();
                    MessageBox.Show(result);
                }
            }
            else
            {
                theJobsControl.Visibility = Visibility.Visible;
            //    MessageBox.Show("Already loaded jobs");
            }
        }
        /// <summary>
        /// Displays the links page and hides other sections.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LinksButton_Click(object sender, RoutedEventArgs e)
        {
            SetThickness(LinksButton);
            currentPageTextBlock.Text = "LINKS";

            theLinksControl.Visibility = Visibility.Hidden;
            theJobsControl.Visibility = Visibility.Hidden;
            theProfilePage.Visibility = Visibility.Hidden;
            WelcomeControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;
            forumControl.Visibility = Visibility.Hidden;
            helpControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;
            if (!LinksLoaded)
            {
                try
                {
                    if (InternetChecker.InternetAvailable())
                    {
                        temporaryBlogGrid.Visibility = Visibility.Visible;
                        FetchText.Text = "Fetching links for you...";
                        jsonObtainer = new JsonObtainer(LinksEndpoint, "Bolkay_Links_Crawler");

                        string jsonResult = await jsonObtainer.GetJsonStringAsync();

                        AfricoderLinksFeed africoderLinksFeed = JsonConvert.DeserializeObject<AfricoderLinksFeed>(jsonResult);
                        //Set the source accordingly.

                        foreach (var dat in africoderLinksFeed.data)
                        {
                            string date = dat.created.date;

                            string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                            dat.created.date = convertedTime;
                            dat.LoggedInID = ID;
                        }
                        theLinksControl.TheListBox.ItemsSource = africoderLinksFeed.data;
                        theLinksControl.Visibility = Visibility.Visible;
                        temporaryBlogGrid.Visibility = Visibility.Hidden;
                        LinksLoaded = true;
                    }
                    else
                    {
                        FetchText.Text = NetworkFailedMessage;
                    }
                }
                catch
                {
                    //Probably a failed message.
                    string result = await jsonObtainer.GetJsonStringAsync();
                    MessageBox.Show(result);
                }
            }
            else
            {
                theLinksControl.Visibility = Visibility.Visible;
                //MessageBox.Show("Links loaded");
            }

        }
        /// <summary>
        /// Displays the tools page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolsButton_Click(object sender, RoutedEventArgs e)
        {
            SetThickness(ToolsButton);
            forumControl.Visibility = Visibility.Hidden;
            theLinksControl.Visibility = Visibility.Hidden;
            theProfilePage.Visibility = Visibility.Hidden;
            theJobsControl.Visibility = Visibility.Hidden;
            WelcomeControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Visible;
            helpControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;
            currentPageTextBlock.Text = "TOOLS";
        }
        /// <summary>
        /// A series of events that gets fired when the application starts without problems.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void theAfricodersWindow_Loaded(object sender, RoutedEventArgs e)
        {

            selectedIndexGrid.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Fires when the user clicks on the profile button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageTextBlock.Text = "Profile Information";
            theProfilePage.Visibility = Visibility.Visible;
            WelcomeControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;
            theLinksControl.Visibility = Visibility.Hidden;
            theJobsControl.Visibility = Visibility.Hidden;
            forumControl.Visibility = Visibility.Hidden;
            helpControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Triggered when the user intends to log-out of the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            ManageCloseLogOut("Are you sure you want to lg out?", "Log out");
        }
        /// <summary>
        /// A simple method to handle the log out/close prompts.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        private void ManageCloseLogOut(string message, string caption)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    mainWindow?.Show();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }
        /// <summary>
        /// Fires when the user clicks on the help button. Nothing fancy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            currentPageTextBlock.Text = "Africoder Help";
            helpControl.Visibility = Visibility.Visible;

            forumControl.Visibility = Visibility.Hidden;
            theLinksControl.Visibility = Visibility.Hidden;
            theProfilePage.Visibility = Visibility.Hidden;
            theJobsControl.Visibility = Visibility.Hidden;
            WelcomeControl.Visibility = Visibility.Hidden;
            blogControl.Visibility = Visibility.Hidden;
            statusControl.Visibility = Visibility.Hidden;
            toolsControl.Visibility = Visibility.Hidden;
            theIndividualForumControl.Visibility = Visibility.Hidden;
        }
    }
}
