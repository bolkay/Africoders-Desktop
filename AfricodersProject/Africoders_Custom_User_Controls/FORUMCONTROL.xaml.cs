using AfricodersProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using AfricodersProject.ForumModel;
using System.Web;

namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for FORUMCONTROL.xaml
    /// </summary>
    public partial class FORUMCONTROL : UserControl
    {
        /// <summary>
        /// The ID of the logged in user.
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// Reference to the json fetcher.
        /// </summary>
        private JsonObtainer jsonObtainer;

        public FORUMCONTROL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button to handle the open forum event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            //Get the categrory
            MultiCategory multiCategory = (MultiCategory)btn.DataContext;
            //Get the category
            string path = multiCategory.path;
            //Get the real endpoint
            
            //MessageBox.Show(path);

            //After clicking the button, it is important to send messgae to the africoders window.
            AfricodersPage africodersPage = Window.GetWindow(Parent) as AfricodersPage;
            //Close all exisitng controls.
            africodersPage.theProfilePage.Visibility = Visibility.Hidden;
            africodersPage.WelcomeControl.Visibility = Visibility.Hidden;
            africodersPage.blogControl.Visibility = Visibility.Hidden;
            africodersPage.statusControl.Visibility = Visibility.Hidden;
            africodersPage.toolsControl.Visibility = Visibility.Hidden;
            africodersPage.theLinksControl.Visibility = Visibility.Hidden;
            africodersPage.theJobsControl.Visibility = Visibility.Hidden;
            africodersPage.forumControl.Visibility = Visibility.Hidden;
            //Only the individual forum control should be alive.
            africodersPage.currentPageTextBlock.Text = "Forum " + multiCategory.board;

            africodersPage.theIndividualForumControl.Path = multiCategory.path;
            //Set the forum ID
            africodersPage.theIndividualForumControl.FID = multiCategory.fid;

            try
            {
                africodersPage.temporaryBlogGrid.Visibility = Visibility.Visible;

                africodersPage.FetchText.Text = "Loading " + multiCategory.board + " forum";
                string endPoint = @"https://api.africoders.com/v1/" + path + "?page=1" + "&order=updated_at|DESC&include=comment";

                jsonObtainer = new JsonObtainer(endPoint, "Forum_Agent_Bolkay");

                string json = await jsonObtainer.GetJsonStringAsync();
                //Changed the explicit Individual forum to dynamic
               IndividualForum individualForum = JsonConvert.DeserializeObject<IndividualForum>(json);
                
                foreach(var dat in individualForum.data)
                {
                    //Reformat the time format.
                    string date = dat.created.date;
                    
                    string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                    dat.created.date = convertedTime;

                    dat.LoggedInID = UserID;
                }

                africodersPage.theIndividualForumControl.TheListBox.ItemsSource = individualForum.data;

                africodersPage.temporaryBlogGrid.Visibility = Visibility.Hidden;
                //Carefully transfer the path to the individual forum
                
                africodersPage.theIndividualForumControl.Visibility = Visibility.Visible;
            }
            catch(Exception t)
            {
                MessageBox.Show(t.Message);
                africodersPage.forumControl.Visibility = Visibility.Visible;
                africodersPage.theIndividualForumControl.Visibility = Visibility.Hidden;
                africodersPage.temporaryBlogGrid.Visibility = Visibility.Hidden;

                MessageBox.Show("Unable to show " + multiCategory.category + " at this time.");
            }
        }
    }
}
