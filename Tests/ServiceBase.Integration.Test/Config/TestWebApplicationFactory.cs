using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ServiceBase.Integration.Test.Config
{
    /// <summary>
    /// Make sure configuration and logging are set.
    /// </summary>
    /// <typeparam name="TStartup"></typeparam>
    public class TestWebApplicationFactory<TStartup>
        :WebApplicationFactory<TStartup> where TStartup: class 
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .Build();
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

                
        }

        
    }
}