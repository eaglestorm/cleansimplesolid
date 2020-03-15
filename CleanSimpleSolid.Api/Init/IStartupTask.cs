using System.Threading;
using System.Threading.Tasks;

namespace ServiceBase.Init
{
    public interface IStartupTask
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}