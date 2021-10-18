using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace todolistReactAsp.Models
{
    public class User : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public override string UserName { get; set; }
        public string Password { get; set; }
        public List<Job> Jobs { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<TokenNotification> TokenNotifications { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}