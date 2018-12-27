using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AfricodersProject.Services
{

    public class AfricodersProber
    {
        /// <summary>
        /// Returns the json obatined after successful sign-in.
        /// </summary>
        public string SuccessJSON { get; set; }
        private string _endpoint;
        private string _message;

        public AfricodersProber(string endpoint, string message)
        {
            EndPoint = endpoint;

            Message = message;
        }

        public string EndPoint
        {
            get => _endpoint;
            set => _endpoint = value ?? throw new Exception("Endpoint cannot be null. Please revalidate");
        }

        public string Message
        {
            get => _message;
            set => _message = value ?? throw new Exception("Please input a sample messaage for the post request");
        }

        public virtual Task<string> AuthenticateLoginAndRegAsync(string usn,string pwd)
        {
            return Task.Run(() => AuthenticateRegisterationandLogin(usn,pwd));
        }

        public virtual string AuthenticateRegisterationandLogin(string usn,string pwd)
        {
            string result = string.Empty;

            WebRequest webRequest = WebRequest.Create(EndPoint);

            HttpWebRequest request = webRequest as HttpWebRequest;

            request.UserAgent = "BOLKAY_FROM_NAIRALAND_GREETINGS";
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.AutomaticDecompression = DecompressionMethods.GZip;

            request.CookieContainer = new CookieContainer();

            var authCredentials = "grant_type=password&userName=" + usn + "&password=" + pwd;
            byte[] encodedMessage = System.Text.Encoding.Default.GetBytes(authCredentials);

            try
            {
                System.IO.Stream stream = request.GetRequestStream();

                stream.Write(encodedMessage, 0, encodedMessage.Length);

                //close the stream
                stream.Close();

                HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse;

                using(Stream theStream = httpWebResponse.GetResponseStream())
                {
                    if (stream != null)
                    {
                        StreamReader reader = new StreamReader(theStream);

                        string t = reader.ReadToEnd();

                        //Get the success json
                        SuccessJSON = t;
                        //Create directory first time.
                        if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Africoders"))
                        {
                            //Create one.
                            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Africoders");
                        }
                        using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Africoders\logBiscuit.txt")) {
                            writer.WriteLine(t);
                        }
                        //Save the username and password in a file
                        using(StreamWriter usPdwriter=new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Africoders\USPLog.txt"))
                        {
                            usPdwriter.WriteLine(usn + " " + pwd);
                        }
                    }
                }
                result = httpWebResponse.StatusDescription;
            }
            catch (Exception e)
            {
                result = e.Message.ToString();
            }

            return result;
        }
    }
}
