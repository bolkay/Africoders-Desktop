namespace AfricodersProject.SinglePostModels
{
    public class Datum
    {
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string slug { get; set; }
        public object url { get; set; }
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
    }
}
