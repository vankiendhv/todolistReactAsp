using System.Collections.Generic;
namespace todolistReactAsp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public List<Job> Jobs { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}