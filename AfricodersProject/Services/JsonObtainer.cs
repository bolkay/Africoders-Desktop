using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AfricodersProject.Services
{
    public class JsonObtainer:IDisposable
    {
        
        public string EndPoint { get; set; }

        public string Agent { get; set; }
        public JsonObtainer(string endpoint,string agent)
        {
            EndPoint = endpoint;

            Agent = agent;
        }

        public Task<string> GetJsonStringAsync()
        {
            return Task.Run(() => GetJsonFromEndpoint());
        }

        public string GetJsonFromEndpoint()
        {
            string result = string.Empty;

            HttpWebRequest request = null;
            //Use web requests.
            WebRequest webRequest = WebRequest.Create(EndPoint);
            
            //Set up the http request
            request = (HttpWebRequest)webRequest;

            //use gzip
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


            //Agent name
            request.UserAgent = Agent;
            
            request.Method = "GET";
            //get respomse from the endpoint
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Is the response successful?
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //Get stream of results
                    using (System.IO.Stream stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                            {
                                if(reader!=null)
                                    result = reader.ReadToEnd();

                                reader.Close();
                            }
                        }
                        stream.Close();
                    }
                }
                else
                {
                    result = "Unable to fetch the blog posts at this time.";
                }
            }
            catch
            {//Nothing nothing nothing 
            }
            return result;
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
        // ~JsonObtainer() {
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
