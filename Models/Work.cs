using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using todolistReactAsp.Controllers;

namespace todolistReactAsp.Models
{
    public class Work : IWork
    {
        private readonly ILogger<Work> logger;
        private int number = 0;

        public Work(ILogger<Work> logger)
        {
            this.logger = logger;
        }
        public async Task DoWork(CancellationToken cancerllationToken)
        {
            // string getDay(string e)
            // {
            //     return e.Length == 1 ? ("0" + e) : e;
            // }
            while (!cancerllationToken.IsCancellationRequested)
            {
                // DateTime now = DateTime.Now;
                // var date = now.Year + "-" + now.Month + "-" + getDay((now.Day).ToString());
                // var time = now.Hour + ":" + now.Minute + ":" + now.Second;
                Interlocked.Increment(ref number);

                // SendTokenController h = new SendTokenController();
                // var ok = h.sendNotification();
                // if (number == 2)
                // {
                //     var defaultApp = FirebaseApp.Create(new AppOptions()
                //     {
                //         Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
                //     });
                //     var message = new Message()
                //     {
                //         Data = new Dictionary<string, string>()
                //         {
                //             ["FirstName"] = "John",
                //             ["LastName"] = "Doe"
                //         },
                //         Notification = new FirebaseAdmin.Messaging.Notification
                //         {
                //             Title = "Thông báo việc làm hôm nay",
                //             Body = "Hôm nay bạn có 2 việc cần phải làm!"
                //         },
                //         Token = "dc1MfsdU-cHBSJToC61bAO:APA91bFwOz9OmTt3tux1zM2i8DcDc1d8JO1d_Q1p1E-QM0ZAbbcbRQIfamCUvHKb_phjJXmLh2Ab2B_BI3Q85v1DTVE3BhNbvec8h1ZMsLx5r29CXelnPg3iu_q14Eccwf618AlK0hCi",
                //         // Topic = "news"
                //     };
                //     var messaging = FirebaseMessaging.DefaultInstance;
                //     var result = await messaging.SendAsync(message);
                logger.LogInformation("ok");
                // }
                // logger.LogInformation($"number: {number}");
                await Task.Delay(1000 * 1);
            }
        }
    }
}