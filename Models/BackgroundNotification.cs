using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace todolistReactAsp.Models
{
    public class BackgroundNotification : IHostedService
    {
        private readonly ILogger<BackgroundNotification> logger;
        private readonly IWork work;

        public BackgroundNotification(ILogger<BackgroundNotification> logger
        , IWork work)
        {
            this.logger = logger;
            this.work = work;
        }



        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await work.DoWork(cancellationToken);
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("end");
            return Task.CompletedTask;
        }
    }
}