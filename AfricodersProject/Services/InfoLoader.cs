using AfricodersProject.AfricoderModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AfricodersProject.Services
{
    public class InfoLoader
    {
       
        public async Task<Datum[]> GetStuffDone(string endpoint,int CurrentPageNumber,int ID)
        {
            AfricoderStatusFeed africoderStatusFeed;
            string ed = endpoint+ CurrentPageNumber.ToString();
            using (JsonObtainer jsonObtainer = new JsonObtainer(ed, "BOLKAY_AGENT"))
            {

                string result = await jsonObtainer.GetJsonStringAsync();

                africoderStatusFeed = JsonConvert.DeserializeObject<AfricoderStatusFeed>(result);

               
                foreach (var bd in africoderStatusFeed.data)
                {
                    bd.LoggedInID = ID;
                    //reformat the html adorned body
                    string b = Regex.Replace(bd.body, @"<[^>]*>", "");

                    bd.body = b;

                    //Find a way to change the date to reflect the current reality.
                    DateTime dateTime = DateTime.Parse(bd.created.date);

                    DateTime currentDate = DateTime.Now;

                    TimeSpan timeSpan = currentDate - dateTime;

                    if (timeSpan.Days <= 1)
                    {
                        bd.created.date = timeSpan.Hours.ToString() + " hours ago";

                    }//Set the date
                    else if (timeSpan.Days > 1)
                    {
                        bd.created.date = timeSpan.Days.ToString() + " days ago";
                    }
                }
            }
            return africoderStatusFeed.data;
        }
    }
}
