using System.Reflection;
using CleanConnect.Common.Model.Settings;
using DbUp;
using DbUp.Engine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NjantPublish.Db.Scripts.Code;

namespace NjantPublish.Db
{
    public class DbUpgrader
    {
        public static void Upgrade(DbSettings dbSettings, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //drop the database and recreate it in dev.
                DeployChanges.To.PostgresqlDatabase((dbSettings.GetConnectionString()))
                    .WithScripts(new IScript[]
                    {
                        new DropScript()
                    })
                    .LogToAutodetectedLog()
                    .Build();
            }

            Update(".Scripts.Schema", dbSettings);
            Update(".Scripts.Data", dbSettings);
        }

        private static void Update(string filter, DbSettings dbSettings)
        {
            var updateder = DeployChanges.To.PostgresqlDatabase(dbSettings.GetConnectionString())
                .WithScriptsEmbeddedInAssembly(Assembly.GetAssembly(typeof(DbUpgrader)),x=>x.Contains(filter))
                .LogToAutodetectedLog()
                .Build();
            
            if (updateder.IsUpgradeRequired())
            {
                updateder.PerformUpgrade();
            }
        }
    }
}