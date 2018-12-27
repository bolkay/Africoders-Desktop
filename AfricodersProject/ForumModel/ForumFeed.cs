using Newtonsoft.Json;
namespace AfricodersProject.ForumModel
{
    [JsonObject]
    public class ForumFeed
    {
        public General General { get; set; }
        [JsonProperty("Web Design")]
        public WebDesign WebDesign { get; set; }
        public Mobile Mobile { get; set; }
        [JsonProperty("App Dev")]
        public AppDev AppDev { get; set; }
        public Database Database { get; set; }
        public Community Community { get; set; }
    }
    public class General
    {
        [JsonProperty("0")]
        public Chat _0 { get; set; }
        [JsonProperty("16")]
        public Africoders _16 { get; set; }
        [JsonProperty("17")]
        public Ideas _17 { get; set; }
    }

    public class Chat
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Africoders
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Ideas
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class WebDesign
    {
        [JsonProperty("1")]
        public _1 _1 { get; set; }
        [JsonProperty("2")]
        public _2 _2 { get; set; }
        [JsonProperty("3")]
        public _3 _3 { get; set; }
        [JsonProperty("4")]
        public _4 _4 { get; set; }
    }

    public class _1
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class _2
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class _3
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class _4
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Mobile
    {
        [JsonProperty("5")]
        public Android _5 { get; set; }
        [JsonProperty("6")]
        public IOS _6 { get; set; }
    }

    public class Android
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class IOS
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class AppDev
    {
        [JsonProperty("7")]
        public Golang _7 { get; set; }
        [JsonProperty("8")]
        public CFamily _8 { get; set; }
        [JsonProperty("9")]
        public Java _9 { get; set; }
        [JsonProperty("10")]
        public Kotlin _10 { get; set; }
        [JsonProperty("11")]
        public DotNet _11 { get; set; }
    }

    public class Golang
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class CFamily
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Java
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Kotlin
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class DotNet
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Database
    {
        [JsonProperty("12")]
        public MYSQL _12 { get; set; }
        [JsonProperty("13")]
        public MSSQL _13 { get; set; }
        [JsonProperty("14")]
        public MongoDb _14 { get; set; }
    }

    public class MYSQL
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class MSSQL
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class MongoDb
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Community
    {
        [JsonProperty("15")]
        public PhpBox _15 { get; set; }
        [JsonProperty("18")]
        public Lounge _18 { get; set; }
        [JsonProperty("19")]
        public FeedBack _19 { get; set; }
    }

    public class PhpBox
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class Lounge
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

    public class FeedBack
    {
        public string category { get; set; }
        public string container { get; set; }
        public string board { get; set; }
        public string path { get; set; }
        public string fid { get; set; }
        public string pid { get; set; }
        public string tid { get; set; }
        public string views { get; set; }
        public string threads { get; set; }
        public string comments { get; set; }
        public string last_post { get; set; }
        public string last_reply { get; set; }
        public string latest { get; set; }
    }

}
