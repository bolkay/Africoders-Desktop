namespace AfricodersProject.AfricoderModels
{
    public class Datum1
    {
        public int id { get; set; }
        public string body { get; set; }
        public string slug { get; set; }
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int shares { get; set; }
        public User1 user { get; set; }
        public string views { get; set; }
        public Created1 created { get; set; }
        public Updated1 updated { get; set; }
        public int? LoggedInID { get; set; }
    }

}
