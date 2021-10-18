namespace todolistReactAsp.Models
{
    public class TokenNotification
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}