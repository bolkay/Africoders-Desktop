using AfricodersProject.UIManagement;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AfricodersProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private static MainWindow instance = null;
        private static readonly object locker=new MainWindow();
        public static MainWindow Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new MainWindow();
                    }
                }
                return instance;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }




        private void MainRegLogWindow_Loaded(object sender, RoutedEventArgs e)
        {

            

            UIManager.Instance.SetTitle(BottomTitleText);
        }
        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            UIManager.Instance.Close(this);
        }
        /// <summary>
        /// Makes the registration page visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void regPageButton_Click(object sender, RoutedEventArgs e)
        {
            UIManager.Instance.ManageRegistrationGroup(LoginControl, RegControl);
            regPageButton.Visibility = Visibility.Hidden;
            LoginPageButton.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Makes the login page visible.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            UIManager.Instance.ManageLoginGroup(LoginControl, RegControl);
            LoginPageButton.Visibility = Visibility.Hidden;
            regPageButton.Visibility = Visibility.Visible;
        }
    }
}
