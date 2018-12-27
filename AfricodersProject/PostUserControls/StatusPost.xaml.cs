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
    /// Interaction logic for StatusPost.xaml
    /// </summary>
    public partial class StatusPost : UserControl
    {
        TextRange range;

        public string Body
        {
            get => range.Text;

            //get
            //{
            //    string temp = string.Empty;

            //    if (range != null)
            //    {
            //       temp= range.Text;
            //    }
            //    return temp;
            //}
        }

        public StatusPost()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {

            RTBox.Document.Blocks.Clear();
        }

        private void shareButton_Click(object sender, RoutedEventArgs e)
        {
            //Sets the textrange
            range = new TextRange(RTBox.Document.ContentStart, RTBox.Document.ContentEnd);
            //get the parent window  
            PostWindow postWindow = Window.GetWindow(Parent) as PostWindow;
            
            postWindow.Close();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //Sets the textrange
            range = new TextRange(RTBox.Document.ContentStart, RTBox.Document.ContentEnd);
        }
    }
}
