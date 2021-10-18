
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using todolistReactAsp.Connection;

namespace todolistReactAsp.Controllers
{
    [Route("api/sendToken")]
    public class SendTokenController : ControllerBase
    {
        private readonly ApplicationContext _context;

        // public SendTokenController()
        // {
        // }

        public SendTokenController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet("{token}")]
        public async Task<IActionResult> GetTokenAsync(string token)
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
            });
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    ["FirstName"] = "John",
                    ["LastName"] = "Doe"
                },
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = "Thông báo việc làm hôm nay",
                    Body = "Hôm nay bạn có 2 việc cần phải làm!"
                },
                Token = token,
                // Topic = "news"
            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            return Ok();
        }
        // private string hello()
        // {
        //     var query = _context.Tag.AsQueryable();
        //     return "ok";
        // }
        // public string sendNotification()
        // {
        //     return "ok";
        // }
    }
}