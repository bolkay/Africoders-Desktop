using AfricodersProject.Africoders_Custom_User_Controls;
using AfricodersProject.AfricodersBlogModel;
using AfricodersProject.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricodersProject.ViewManagement
{
    public class BlogManagement
    {
        public virtual async Task ManageBlogPostsAsync(JsonObtainer jsonObtainer, BlogFeedControl blogFeedControl, int ID)
        {
            string jsonResult = await jsonObtainer.GetJsonStringAsync();

            AfricoderBlogFeed africoderBlogFeed = JsonConvert.DeserializeObject<AfricoderBlogFeed>(jsonResult);

            if (jsonResult != string.Empty)
            {
                foreach (var result in africoderBlogFeed.data)
                {
                    string date = result.created.date;


                    string convertedTime = Convert.ToDateTime(DateTime.Parse(date)).ToString(("ddd, dd MMM yyyy hh:mm:tt"));
                    result.created.date = convertedTime;
                }

                foreach (var dat in africoderBlogFeed.data)
                {
                    dat.LoggedInID = ID;
                }
                blogFeedControl.TheLB.ItemsSource = africoderBlogFeed.data;
            }
        }
    }
}
