using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricodersProject.UIManagement
{
    public class UIManager
    {
        #region Singleton
        private static UIManager instance = null;
        private static readonly object locker = new UIManager();

        public static UIManager Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new UIManager();
                    }
                }
                return instance;
            }
        }
        #endregion

        public void CleanTextBoxes<T>(T[] textboxes) where T : System.Windows.Controls.TextBox
        {
            //Eradicate the ugly textbox text on start
            foreach(var textbox in textboxes)
            {
                textbox.Clear();
            }
        }

        public void SetTitle<T>(T textblock)where T : System.Windows.Controls.TextBlock
        {
            string title = "Africoders Desktop Client For User --";
            string OsName = Environment.UserDomainName;
            string date = DateTime.Now.ToShortDateString();
            //Set the title
            textblock.Text = title + " " + OsName + " --" + date;
        }

        public void Close(System.Windows.Window window)
        { 
            window.Close();
        }

        public void ManageRegistrationGroup(System.Windows.Controls.UserControl loginControl, System.Windows.Controls.UserControl regControl)
        {
            //Set login to false
            loginControl.Visibility = System.Windows.Visibility.Hidden;
            //Set reg to true
            regControl.Visibility = System.Windows.Visibility.Visible;
        }

        public void ManageLoginGroup(System.Windows.Controls.UserControl loginControl, System.Windows.Controls.UserControl regControl)
        {
            //Set login to false
            loginControl.Visibility = System.Windows.Visibility.Visible;
            //Set reg to true
            regControl.Visibility = System.Windows.Visibility.Hidden;
        }

        public void ViewContactPage()
        {

            Process.Start("mailto:support@africoders.com");

            var processes=Process.GetProcesses();

            foreach(Process process in processes)
            {
                process.Dispose();
                process.Close();
            }
        }

        public async void SetTermsOfuse<T>(T textBox) where T : System.Windows.Controls.TextBox
        {

            using (StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory()+@"\DocumentFiles\AfricoderTerms.txt"))
            {
                string thing = await streamReader.ReadToEndAsync();

                textBox.Text = thing;
            }
        }
    }
}
