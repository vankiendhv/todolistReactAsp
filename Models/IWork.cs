using System.Threading;
using System.Threading.Tasks;

namespace todolistReactAsp.Models
{
    public interface IWork
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}