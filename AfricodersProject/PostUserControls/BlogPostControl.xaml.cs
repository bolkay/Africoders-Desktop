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
    /// Interaction logic for BlogPostControl.xaml
    /// </summary>
    public partial class BlogPostControl : UserControl
    {
        /// <summary>
        /// Range of texts in the rich text box.
        /// </summary>
        private TextRange textRange;
        /// <summary>
        /// Designates the title of the post, as entered by the user.
        /// </summary>
        public string PostTitle
        {
            get => TitleBox.Text;
        }
        /// <summary>
        /// Designates the body of the pist, as entered by the user.
        /// </summary>
        public string PostBody
        {

            get
            {
                return textRange.Text;
            }
            //{
            //    string temp = string.Empty;
            //    if (textRange != null) { temp = textRange.Text; }
            //    return temp;
            //}
        }
        public BlogPostControl()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            RTBox.Document.Blocks.Clear();
        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            PostWindow postWindow = Window.GetWindow(Parent) as PostWindow;
            textRange = new TextRange(RTBox.Document.ContentStart, RTBox.Document.ContentEnd);
            postWindow.Close();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            textRange = new TextRange(RTBox.Document.ContentStart, RTBox.Document.ContentEnd);

        }
    }
}
