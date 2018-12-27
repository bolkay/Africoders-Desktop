using System.Net;
using System.IO;
using System.Threading.Tasks;

namespace AfricodersProject.Services
{
    /// <summary>
    /// This class will be called by the concerned categories to make a post. E.g. to the status board.
    /// </summary>
    public class PostHelper
    {
        /// <summary>
        /// The endpoint of the status feed to make a post to.
        /// </summary>
        public string Endpoint { get; set; }
        /// <summary>
        /// Did we make a sucessful request?
        /// </summary>
        public bool StatusPostSuccessful { get; set; }

        public PostHelper(string statusEnd)
        {
            Endpoint = statusEnd;
        }

        /// <summary>
        /// Make status asynchronously. This is preferred.
        /// </summary>
        /// <param name="token">Unique token of the logged in user.</param>
        /// <returns></returns>
        public Task MakePostAsync(string token)
        {
            return Task.Run(() => MakePost(token));
        }
        /// <summary>
        /// Make a status on Africoders.com
        /// </summary>
        /// <param name="token">Token is an important parameter. Method obviously won't work without it.</param>
        public virtual void MakePost(string token)
        {
            string dummyMessage = "This is just a dummy post request.";

            HttpWebRequest request = null;
            //The endpoint will be supplied at the individual category level.
            WebRequest webRequest = WebRequest.Create(Endpoint);

            request = webRequest as HttpWebRequest;//Cast the webrequest to http status.

            request.Credentials = CredentialCache.DefaultCredentials;//use default credentials.

            //Post method.
            request.Method = "POST";

            request.Accept = "application/json";

            //request.Headers.Add(HttpRequestHeader.Accept, "application/json");

            request.Headers.Add(HttpRequestHeader.Authorization, token);

            try
            {

                Stream stream = request.GetRequestStream();

                stream.Write(System.Text.Encoding.UTF8.GetBytes(dummyMessage), 0, System.Text.Encoding.UTF8.GetBytes(dummyMessage).Length);

                //get the response stream
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;


                if (response.StatusCode == HttpStatusCode.OK)
                {

                    StatusPostSuccessful = true;
                    //close the response
                    response.Close();
                    //close the stream
                    stream.Close();

                }
            }
            catch
            {
                StatusPostSuccessful = false;
            }


        }
    }
}
