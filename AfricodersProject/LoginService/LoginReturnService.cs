using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfricodersProject.LoginService
{
    /// <summary>
    /// This is a very important class that handles the json results obtained from the server, after a successful login request.
    /// </summary>
    public class LoginReturnService
    {
        public Datum[] data { get; set; }
        public string status { get; set; }
        public string token { get; set; } //Token for some important requests.

        public class Datum
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string vanity { get; set; }
            public string gender { get; set; }
            public string avatarUrl { get; set; }
            public string profileUrl { get; set; }
            public string first { get; set; }
            public string last { get; set; }
            public string bio { get; set; }
            public string url { get; set; }
            public string company { get; set; }
            public string location { get; set; }
            public string occupation { get; set; }
            public string phone { get; set; }
            public Created created { get; set; }
            public Updated updated { get; set; }
            public string birthdate { get; set; }
        }

        public class Created
        {
            public string date { get; set; }
            public int timezone_type { get; set; }
            public string timezone { get; set; }
        }

        public class Updated
        {
            public string date { get; set; }
            public int timezone_type { get; set; }
            public string timezone { get; set; }
        }

    }
}
