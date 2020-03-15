using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBase.Init
{
    public static class WebHostExtensions
    {
        public static async Task RunTasksAsync(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                // Load all tasks from DI
                var startupTasks = webHost.Services.GetServices<IStartupTask>();

                // Execute all the tasks
                foreach (var startupTask in startupTasks)
                {
                    await startupTask.ExecuteAsync();
                }
            }
        }
    }
}