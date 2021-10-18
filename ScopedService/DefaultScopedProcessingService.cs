using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using todolistReactAsp.Connection;

namespace todolistReactAsp.ScopedService
{
    public class DefaultScopedProcessingService : IHostedService, IScopedProcessingService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private int _executionCount;
        private readonly ILogger<DefaultScopedProcessingService> _logger;

        public DefaultScopedProcessingService(ILogger<DefaultScopedProcessingService> logger, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public string getDay(string e)
        {
            return e.Length == 1 ? ("0" + e) : e;
        }
        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
            });
            SendFirebase sendFirebase = new SendFirebase();

            while (!stoppingToken.IsCancellationRequested)
            {
                ++_executionCount;
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    var _tokenWithUser = dbContext.User.Where(a => a.EmailConfirmed == true)
                    .Include(n => n.TokenNotifications)
                    .Select(x => new
                    {
                        x.Id,
                        TokenNotification = x.TokenNotifications.Select(m => m.Token)
                    }).ToArray();
                    Array GetJobToday(int id)
                    {
                        DateTime now = DateTime.Now;
                        var date = now.Year + "-" + now.Month + "-" + getDay((now.Day).ToString());
                        var query = dbContext.Job.Where(n => n.UserId == id && n.Time == date)
                        .Select(x => new
                        {
                            x.Id,
                            x.Name,
                            x.Important,
                            x.Time,
                            x.UserId,
                        }).ToArray();
                        return query;
                    }

                    for (int i = 0; i < _tokenWithUser.Length; i++)
                    {
                        var element = _tokenWithUser[i];
                        string body = $"Hôm nay bạn có {GetJobToday(element.Id).Length} công việc cần phải làm!";
                        var tokenNotificationArr = element.TokenNotification.ToArray();

                        for (int j = 0; j < tokenNotificationArr.Length; j++)
                        {
                            var token = tokenNotificationArr[j];
                            await sendFirebase.SendNotificationAsync(token, "Thông báo việc làm hôm nay", body);
                            _logger.LogInformation(token);
                        }
                    }
                }
                await Task.Delay(50_000, stoppingToken);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}