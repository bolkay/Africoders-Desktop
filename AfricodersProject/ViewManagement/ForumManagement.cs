using AfricodersProject.Africoders_Custom_User_Controls;
using AfricodersProject.ForumModel;
using AfricodersProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AfricodersProject.ViewManagement
{
    public class ForumManagement
    {
        public virtual async Task ForumBoardManagement( JsonObtainer jsonObtainer, FORUMCONTROL forumControl)
        {
            //Get the json
            string json = await jsonObtainer.GetJsonStringAsync();

            //map the obtained json to forum feed.
            ForumFeed forumFeed = Newtonsoft.Json.JsonConvert.DeserializeObject<ForumFeed>(json);

            //use the mapper class to get the general category
            ForumMapper forumMapper = new ForumMapper();

            //Get the list of general categories from the forum feed.
            var generalCategories = forumMapper.GeneralCategories(forumFeed);
            //Get the list of web design categories
            var webCategories = forumMapper.WebCategories(forumFeed);
            //Get the mobile categories
            var mobileCategories = forumMapper.MobileCategories(forumFeed);
            //Finally set the datasource
            //Load the app categories
            var appCategories = forumMapper.AppCategories(forumFeed);

            //Get the database categories
            var databaseCategories = forumMapper.DatabaseCategories(forumFeed);

            //get the commmunity categories
            var communityCategories = forumMapper.CommunityCategories(forumFeed);
            foreach (var dat in webCategories)
            {
                string text = dat.latest;

                string formatted = Regex.Replace(text, @"<[^>]*>", "");
                dat.latest = formatted;
            }
            foreach (var dat in generalCategories)
            {
                string text = dat.latest;

                string formatted = Regex.Replace(text, @"<[^>]*>", "");
                dat.latest = formatted;
            }
            foreach (var dat in mobileCategories)
            {
                string text = dat.latest;

                string formatted = Regex.Replace(text, @"<[^>]*>", "");
                dat.latest = formatted;
            }
            foreach (var dat in appCategories)
            {
                string text = dat.latest;

                string formatted = Regex.Replace(text, @"<[^>]*>", "");
                dat.latest = formatted;
            }

            foreach (var dat in databaseCategories)
            {
                string text = dat.latest;

                string formatted = Regex.Replace(text, @"<[^>]*>", "");
                dat.latest = formatted;
            }
            foreach (var dat in communityCategories)
            {
                string text = dat.latest;

                string formatted = Regex.Replace(text, @"<[^>]*>", "");
                dat.latest = formatted;
            }
            forumControl.TheGeneralForumListBox.ItemsSource = generalCategories;

            forumControl.WebGeneralForumListBox.ItemsSource = webCategories;

            forumControl.MobileGeneralForumListBox.ItemsSource = mobileCategories;

            forumControl.AppGeneralForumListBox.ItemsSource = appCategories;

            forumControl.DatabaseGeneralForumListBox.ItemsSource = databaseCategories;

            forumControl.CommunityGeneralForumListBox.ItemsSource = communityCategories;


        }
    }
}
