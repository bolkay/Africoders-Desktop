using AfricodersProject.Services;
using AfricodersProject.UIManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using AfricodersProject.LoginService;
using System.Windows.Media.Imaging;
using DataConverters;
using System.IO;

namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        /// <summary>
        /// Important reference to the Africoders prober class.
        /// </summary>
        AfricodersProber africoders;
        public LoginControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Cleans textboxes when the control starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UIManager.Instance.CleanTextBoxes(new TextBox[] { LoginNameTextBox, LoginPassTextBox });
        }

        /// <summary>
        /// Fires when the user expresses intent to login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            logginText.Text = "Please hold on!";
            string username = LoginNameTextBox.Text; //get the username

            string password = LoginPassTextBox.Text; //get the password.

            string endpoint = @"https://api.africoders.com/v1/user/login?name=" + username + "&password=" + password;

            africoders = new AfricodersProber(endpoint, "Log in user");

            string result = await africoders.AuthenticateLoginAndRegAsync(username,password);

            if (result == "OK")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Proceed to your Africoders Page?", "User successfully Authenticated", MessageBoxButton.YesNo, MessageBoxImage.Question);
                switch (messageBoxResult)
                {
                    case MessageBoxResult.Yes:
                        break;
                    case MessageBoxResult.No:
                        return;

                }

                logginText.Text = "Successfully signed in!";
                //Now, we have to get the success json returned from the server.
                //This result contains all the important information about the currently signed-in user.
                string successJsonResult = africoders.SuccessJSON;
                LoginReturnService loginReturnService=null;
                //Carefully deserialise the json result using the login return service class.
                if (successJsonResult != null)
                {
                    loginReturnService = JsonConvert.DeserializeObject<LoginReturnService>(successJsonResult);
                }
                //MessageBox.Show(loginReturnService.status + " " + loginReturnService.token);
                
                //Carefully load the africoders page
                AfricodersPage africodersPage = new AfricodersPage();

                africodersPage.WelcomeControl.username.Text = loginReturnService.data.Select(x=>x.name).FirstOrDefault();
                
                var bitmap = new BitmapImage(new Uri(loginReturnService.data.Select(x => x.avatarUrl).FirstOrDefault()));

                africodersPage.WelcomeControl.profilePicture.Source = bitmap;

                africodersPage.currentPageTextBlock.Text = "Welcome " + username + " !";

                //IMPORTANT
                
                //SET THE TOKEN FOR THE STATUS CONTROL
                africodersPage.statusControl.Token = loginReturnService.token;//GET THE TOKEN FOR THE AUTHENTICATED USER.
                
                //SET TOKEN FOR THE BLOG CONTROL
                africodersPage.blogControl.Token = loginReturnService.token;

                //Set token for the Jobs control.
                africodersPage.theJobsControl.Token = loginReturnService.token;
 
                //Set token for the links control.
                africodersPage.theLinksControl.Token = loginReturnService.token;

                //Set token for the individual forum control.
                africodersPage.theIndividualForumControl.Token = loginReturnService.token;
                //Here set all profile information accordingly.
                //Profile picture
                africodersPage.theProfilePage.ProfilePicture.Source = bitmap;
                //Nickname
                africodersPage.theProfilePage.nickNameText.Text = loginReturnService.data.Select(x => x.name).FirstOrDefault() ?? "Not set.";
                //firstname
                africodersPage.theProfilePage.firstNameText.Text = loginReturnService.data.Select(f => f.first).FirstOrDefault() ?? "Not set.";
                //lastName
                africodersPage.theProfilePage.lastNameText.Text = loginReturnService.data.Select(l => l.last).FirstOrDefault() ?? "Not set.";
                //Phone
                africodersPage.theProfilePage.phoneText.Text = loginReturnService.data.Select(p => p.phone).FirstOrDefault() ?? "Not set.";
                //Email
                africodersPage.theProfilePage.emailText.Text = loginReturnService.data.Select(em=> em.email).FirstOrDefault() ?? "Not set.";
                //gender
                africodersPage.theProfilePage.genderText.Text = loginReturnService.data.Select(gd => gd.gender).FirstOrDefault() ?? "Not set.";
                //location
                africodersPage.theProfilePage.locationText.Text = loginReturnService.data.Select(lc => lc.location).FirstOrDefault() ?? "Not set.";
                //occupation
                africodersPage.theProfilePage.occupationText.Text = loginReturnService.data.Select(oc => oc.occupation).FirstOrDefault() ?? "Not set.";
                //birthday
                africodersPage.theProfilePage.birthdayText.Text = loginReturnService.data.Select(bd=> bd.birthdate).FirstOrDefault() ?? "Not set.";
                //company
                africodersPage.theProfilePage.companyText.Text = loginReturnService.data.Select(com => com.company).FirstOrDefault() ?? "Not set.";

                //Get the ID of the authorised user. This is very important.
                int generalID = loginReturnService.data.Select(x => x.id).FirstOrDefault();
                africodersPage.ID = generalID;


                //MessageBox.Show(africodersPage.ID.ToString());
                //Propagate the ID across all controls.

                africodersPage.statusControl.LoggedID =generalID;

                africodersPage.theJobsControl.LoggedId=generalID;
                                                     
                africodersPage.theLinksControl.LoggedID=generalID;

                africodersPage.blogControl.LoggedID  = generalID;

                africodersPage.theIndividualForumControl.LoggedID = generalID;

                africodersPage.forumControl.UserID = generalID;
                //Set the top proile image
                africodersPage.topProfileImage.Source = bitmap;
                africodersPage.topIconImage.ToolTip =loginReturnService.data.Select(n => n.name).FirstOrDefault();
                africodersPage.Show();

                //Hide the main window
                MainWindow mainWindow = Window.GetWindow(Parent) as MainWindow;
                africodersPage.mainWindow = mainWindow;
                mainWindow.Hide();
            }
            else
            {

                logginText.Text = "Authentication Failed!. Consider trying again.";
                MessageBox.Show("Unable to authenticate user because: " +"\n"+ result);
            }
        }

        /// <summary>
        /// Search if past records of the user exits on system.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lastRecordButton_Checked(object sender, RoutedEventArgs e)
        {
            string path= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Africoders\USPLog.txt";
            //Get last known record.
            //Does it exist?
            if (File.Exists(path))
            {
                using(StreamReader reader=new StreamReader(path))
                {
                    string[] UsernameAndPassword = reader.ReadToEnd().Split(new char[] { ' ' });
                    LoginNameTextBox.Text = UsernameAndPassword[0];
                    LoginPassTextBox.Text = UsernameAndPassword[1];
                    
                }
            }
            else
            {
                lastRecordButton.IsChecked = false;
                //The user will have to manually type his or her credentials. This will only happen the first time assuming all other factors are constant.
                MessageBox.Show("No past record found. Consider typing your credentials manually.","No Exisiting User Found",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
    }
}
