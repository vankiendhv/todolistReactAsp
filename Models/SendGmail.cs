using System.Collections.Generic;
namespace todolistReactAsp.Models
{
    public class SendGmail
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Gmail { get; set; }
        public string Password { get; set; }
    }
}