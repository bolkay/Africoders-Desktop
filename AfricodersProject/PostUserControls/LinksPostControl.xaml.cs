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

namespace AfricodersProject.PostUserControls
{
    /// <summary>
    /// Interaction logic for LinksPostControl.xaml
    /// </summary>
    public partial class LinksPostControl : UserControl
    {
        /// <summary>
        /// Text range for capturing the URL text box
        /// </summary>
        TextRange textRange1;
        /// <summary>
        /// Text range for the body textbox
        /// </summary>
        TextRange textRange2;
        /// <summary>
        /// Url to post.
        /// </summary>
        public string URL
        {
            get => textRange1.Text;
        }

        /// <summary>
        /// Body of the post.
        /// </summary>
        public string Body
        {
            get => textRange2.Text;
        }
        /// <summary>
        /// Title of the link post.
        /// </summary>
        public string Title
        {
            get => TitleBox.Text;
        }
        public LinksPostControl()
        {
            
            InitializeComponent();
        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            PostWindow postWindow = Window.GetWindow(Parent) as PostWindow;
            //URL text range
            textRange1 = new TextRange(URLTBox.Document.ContentStart, URLTBox.Document.ContentEnd);
            //Body text range.
            textRange2 = new TextRange(RTBox.Document.ContentStart, RTBox.Document.ContentEnd);

            postWindow.Close();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            //Clear the body textbox
            RTBox.Document.Blocks.Clear();
            //clear the url textbox
            URLTBox.Document.Blocks.Clear();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //URL text range
            textRange1 = new TextRange(URLTBox.Document.ContentStart, URLTBox.Document.ContentEnd);
            //Body text range.
            textRange2 = new TextRange(RTBox.Document.ContentStart, RTBox.Document.ContentEnd);

        }
    }
}
