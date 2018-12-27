using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System;
using System.Windows;

namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for EncoderUserControl.xaml
    /// </summary>
    public partial class EncoderUserControl : System.Windows.Controls.UserControl
    {

        private string result = string.Empty;

        public EncoderUserControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// For now, there shall be two encoding methods.
        /// </summary>
        private enum EncodingMethods
        {
            SHA1,

            MD5,
           
        };

        /// <summary>
        /// Fires when the control starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            selectionComboBox.ItemsSource = Enum.GetValues(typeof(EncodingMethods));

            //set default
            selectionComboBox.SelectedIndex = 0;
        }

        //Reusable
        /// <summary>
        /// Fires when the user choses SHA1.
        /// </summary>
        /// <param name="text"></param>
        private void EncodeTextUsingSHA1(string text)
        {

            SHA1 sHA1 = SHA1.Create();
            
            byte[] arr = Encoding.UTF8.GetBytes(text);

            byte[] computedHash = sHA1.ComputeHash(arr);

            result = BitConverter.ToString(computedHash);

            resultTextBox.Text = result.Replace("-", "");

            successTB.Text = "Sucessfully encoded text using SHA1.";
        }
        /// <summary>
        /// Fires if the users choses MD5.
        /// </summary>
        /// <param name="text"></param>
        private void EncodeTextUsingMD5(string text)
        {

            MD5 mD5 = MD5.Create();

            byte[] arr = Encoding.UTF8.GetBytes(text);

            byte[] computedHash = mD5.ComputeHash(arr);

            result = BitConverter.ToString(computedHash);

            resultTextBox.Text = result.Replace("-","");

            successTB.Text = "Sucessfully encoded text using MD5.";

           
        }
        /// <summary>
        /// Handles the encoding process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void encodeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            switch (selectionComboBox.SelectedIndex)
            {
                case 0:
                    EncodeTextUsingSHA1(theTextBox.Text);
                    break;
                    case 1:
                    EncodeTextUsingMD5(theTextBox.Text);
                    break;
                default:
                    successTB.Text = "Error computing hash.";
                    break;
            }
        }
    }
}
