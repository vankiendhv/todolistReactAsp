using System.Threading;
using System.Threading.Tasks;

namespace todolistReactAsp.ScopedService
{
    public interface IScopedProcessingService
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}