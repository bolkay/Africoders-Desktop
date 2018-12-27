using AfricodersProject.AfricoderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AfricodersProject.AfricodersBlogModel;
using Datum = AfricodersProject.AfricoderModels.Datum;
using AfricodersProject.AfricoderJobModel;
using AfricodersProject.AfricoderLinksModel;
using AfricodersProject.ForumModel;

namespace AfricodersProject.Services
{
    /// <summary>
    /// This class will attend to the pagination services for each category of concern.
    /// </summary>
    public class PaginationHelper:IDisposable
    {
        /// <summary>
        /// The pagination class helper constructor that takes in the endpoint parameter.
        /// </summary>
        public PaginationHelper(string endPoint)
        {
            //Get the endpoint.
            Endpoint = endPoint;
        }
        /// <summary>
        /// Endpoint of the concerned category. E.g. Status
        /// The endpoint must end with an = sign for this logic to work.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// A reference to the JSON obatainer class.
        /// </summary>
        private static JsonObtainer obtainer;
        /// <summary>
        /// A reference to the status feed.
        /// </summary>
        AfricoderStatusFeed africoderStatusFeed;
        /// <summary>
        /// Obtain reference to the blog feed.
        /// </summary>
        AfricoderBlogFeed africoderBlogFeed;
        /// <summary>
        /// Helper function for navigating through statuses. It is necessary to separate the functions because of the nature of results obtained from the API.
        /// </summary>
        /// <param name="currentIndex"></param>
        
        //Reference to the jobs feed.
        AfricoderJobsFeed africoderJobsFeed;
        /// <summary>
        /// Reference to the africoders links feed.
        /// </summary>
        private AfricoderLinksFeed africoderLinksFeed;
        /// <summary>
        /// Reference to the individual forum.
        /// </summary>
        private IndividualForum individualForum;
        public virtual async Task<Datum[]> GetNextStatusPosts(int currentIndex,int ID)
        {
            string jsonResult = string.Empty;
            //Paginate the status as necessary.
            Endpoint = Endpoint + currentIndex.ToString();

            //Create new obtainer
            obtainer = new JsonObtainer(Endpoint, "Status Pagination Agent");

            jsonResult = await obtainer.GetJsonStringAsync();

            africoderStatusFeed = JsonConvert.DeserializeObject<AfricoderStatusFeed>(jsonResult);
            foreach(var dat in africoderStatusFeed.data)
            {
                dat.LoggedInID = ID;
            }
            return africoderStatusFeed.data;
        }

        public virtual async Task<AfricoderJobsFeed.Datum[]> GetNextJobsPosts(int currentIndex,int ID)
        {
            string jsonResult = string.Empty;
            //Paginate the status as necessary.
            Endpoint = Endpoint + currentIndex.ToString();

            //Create new obtainer
            obtainer = new JsonObtainer(Endpoint, "Status Pagination Agent");

            jsonResult = await obtainer.GetJsonStringAsync();

            africoderJobsFeed = JsonConvert.DeserializeObject<AfricoderJobsFeed>(jsonResult);

            foreach(var dat in africoderJobsFeed.data)
            {
                dat.LoggedID = ID;
            }

            return africoderJobsFeed.data;
        }


        public virtual async Task<AfricodersBlogModel.Datum[]> GetNextBlogPosts(int currentIndex,int ID)
        {
            Endpoint = Endpoint + currentIndex.ToString();
            africoderBlogFeed = new AfricoderBlogFeed();
            obtainer = new JsonObtainer(Endpoint, "Blog Pagination Agent");
            string jsonresult = await obtainer.GetJsonStringAsync();
            africoderBlogFeed = JsonConvert.DeserializeObject<AfricoderBlogFeed>(jsonresult);

            foreach(var dat in africoderBlogFeed.data)
            {
                dat.LoggedInID = ID;
            }
            return africoderBlogFeed.data;
        }

        public virtual async Task<AfricoderLinksModel.Datum[]> GetNextLinkPosts(int currentIndex,int ID)
        {
            Endpoint = Endpoint + currentIndex.ToString();
            africoderLinksFeed = new AfricoderLinksFeed();
            obtainer = new JsonObtainer(Endpoint, "Blog Pagination Agent");
            string jsonresult = await obtainer.GetJsonStringAsync();
            africoderLinksFeed = JsonConvert.DeserializeObject<AfricoderLinksFeed>(jsonresult);

            foreach(var dat in africoderLinksFeed.data)
            {
                dat.LoggedInID = ID;
            }
            return africoderLinksFeed.data;
        }
        public virtual async Task<List<ForumModel.Datum>> GetNextForumPosts(int currentIndex, int ID)
        {
            //Endpoint = Endpoint + currentIndex.ToString();
            africoderLinksFeed = new AfricoderLinksFeed();
            obtainer = new JsonObtainer(Endpoint, "Blog Pagination Agent");
            string jsonresult = await obtainer.GetJsonStringAsync();
            individualForum = JsonConvert.DeserializeObject<IndividualForum>(jsonresult);

            foreach (var dat in individualForum.data)
            {
                dat.LoggedInID = ID;
            }
            return individualForum.data;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PaginationHelper() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
