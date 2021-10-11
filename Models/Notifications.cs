namespace todolistReactAsp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int userId { get; set; }
        public User User { get; set; }
    }
}