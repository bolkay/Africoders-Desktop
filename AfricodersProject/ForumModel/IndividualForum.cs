namespace AfricodersProject.ForumModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
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

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string vanity { get; set; }
        public string avatarUrl { get; set; }
        public string profileUrl { get; set; }
    }

    public class User2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string vanity { get; set; }
        public string avatarUrl { get; set; }
        public string profileUrl { get; set; }
    }

    public class Created2
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Updated2
    {
        public string date { get; set; }
        public int timezone_type { get; set; }
        public string timezone { get; set; }
    }

    public class Datum2
    {
        public int id { get; set; }
        public string body { get; set; }
        public string slug { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int shares { get; set; }
        public User2 user { get; set; }
        public string views { get; set; }
        public Created2 created { get; set; }
        public Updated2 updated { get; set; }
        public int? LoggedInID { get; internal set; }
    }

    public class Comment
    {
        public List<Datum2> data { get; set; }
    }

    public class Datum
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int shares { get; set; }
        public string views { get; set; }
        public string replies { get; set; }
        public string category { get; set; }
        public Created created { get; set; }
        public Updated updated { get; set; }
        public string published { get; set; }
        public User user { get; set; }
        public Comment comment { get; set; }
        public int? LoggedInID { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public List<object> links { get; set; }
    }

    public class Meta
    {
        [JsonIgnore]
        public Pagination pagination { get; set; }
    }

    public class IndividualForum
    {
        public List<Datum> data { get; set; }
        [JsonIgnore]
        public Meta meta { get; set; }
    }
}
