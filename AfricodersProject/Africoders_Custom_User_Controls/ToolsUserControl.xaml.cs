using System;
using System.Collections.Generic;
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

namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for ToolsUserControl.xaml
    /// </summary>
    public partial class ToolsUserControl : UserControl
    {
        public ToolsUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fires when the control is focused.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Set the year text
            yearText.Text = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Loads up the string encoder tool window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encodingButton_Click(object sender, RoutedEventArgs e)
        {
            OtherToolsWindow otherToolsWindow = new OtherToolsWindow();
            otherToolsWindow.titleText.Text = "Simple Text Encoder";
            otherToolsWindow.Show();
        }
        /// <summary>
        /// Loads up the PDF maker window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PdfmakerButton_Click(object sender, RoutedEventArgs e)
        {
            ToolsWindow toolsWindow = new ToolsWindow();
            toolsWindow.Show();
        }
    }
}
