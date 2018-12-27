using AfricodersProject.Services;
using AfricodersProject.SinglePostModels;
using Newtonsoft.Json;
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
using Newtonsoft.Json.Linq;
namespace AfricodersProject.Africoders_Custom_User_Controls
{
    /// <summary>
    /// Interaction logic for SinglePostControl.xaml
    /// </summary>
    public partial class SinglePostControl : UserControl
    {
        
        public SinglePostControl()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string ed = @"https://api.africoders.com/v1/posts?category=blog&include=comment:order(id|asc)&order=id|asc&limit=10";
            JsonObtainer jsonObtainer = new JsonObtainer(ed, "From_BOLKAY");
            string json = await jsonObtainer.GetJsonStringAsync();
            AfricoderSinglePost africoderSinglePost = JsonConvert.DeserializeObject<AfricoderSinglePost>(json);

            //JObject jObject = JObject.Parse(json);

            //JArray jArray = (JArray)jObject["data"];

            //IList<AfricoderSinglePost> africoderSinglePosts = jArray.ToObject<IList<AfricoderSinglePost>>();

            TheLB.ItemsSource = africoderSinglePost.data;


            MessageBox.Show("Sucess");
        }
    }
}
