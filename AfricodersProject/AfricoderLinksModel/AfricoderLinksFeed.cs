namespace AfricodersProject.AfricoderLinksModel
{

    public class AfricoderLinksFeed
    {
        public Datum[] data { get; set; }
        public Meta meta { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
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
        public int? LoggedInID { get; set; }
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

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string vanity { get; set; }
        public string avatarUrl { get; set; }
        public string profileUrl { get; set; }
    }
    
}
