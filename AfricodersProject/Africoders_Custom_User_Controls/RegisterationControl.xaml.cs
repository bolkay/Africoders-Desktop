using AfricodersProject.Services;
using AfricodersProject.UIManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for RegisterationControl.xaml
    /// </summary>
    public partial class RegisterationControl : UserControl
    {
        public RegisterationControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clean the default values of the textboxes upn start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UIManager.Instance.CleanTextBoxes(new TextBox[] { regEmailTextBox, regNameTextBox, regPassTextBox });
        }

        /// <summary>
        /// Fires when the user intends to make new registration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            holdOnText.Text = "Please hold on!";
            string username = regNameTextBox.Text;
            string password = regPassTextBox.Text;
            string email = string.Empty;
            //Simple regex to determine if email address is valid.
            if (Regex.IsMatch(regEmailTextBox.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"))
            {
                email = regEmailTextBox.Text;
                //The registration endpoint.
                string endpoint = "https://api.africoders.com/v1/user/signup?name=" + username + "&password=" + password + "&email=" + email;

                //Reference to the africoders prober class.
                AfricodersProber prober = new AfricodersProber(endpoint, "Register a new user");

                string result = await prober.AuthenticateLoginAndRegAsync(username,password);
                //Is the registration successful?
                if (result == "OK")
                {
                    MessageBox.Show(("Africoders Account successfully Created for: " + username + ". Visit the Login Page to Sign in to your new account."), "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                holdOnText.Text = "An error has occured";
                MessageBox.Show("Provided email address not in a correct format. Please revalidate.", "Email Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }
    }
}
