using System.Threading.Tasks;
using FirebaseAdmin.Messaging;

namespace todolistReactAsp.ScopedService
{
    public class SendFirebase
    {
        public async Task SendNotificationAsync(string DeviceToken, string title, string body)
        {
            var message = new Message()
            {
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = body
                },
                Token = DeviceToken,
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
        }
    }
}