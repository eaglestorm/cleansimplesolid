using System.Threading;
using System.Threading.Tasks;
using CleanConnect.Common.Model.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NjantPublish.Db;

namespace ServiceBase.Init
{
    public class DbMigrationTask: IStartupTask
    {
        private readonly IHostingEnvironment _environment;
        private DbSettings _dbSettings;
        private ILogger _logger;
        
        public DbMigrationTask(IOptions<DbSettings> options, IHostingEnvironment environment, ILogger<DbMigrationTask> logger)
        {
            _environment = environment;
            _dbSettings = options.Value;
            _logger = logger;
        }
        
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Starting upgrade test.");
            DbUpgrader.Upgrade(_dbSettings,_environment);
            return Task.CompletedTask;
        }
    }
}