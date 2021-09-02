using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using ServiceBase.Init;

namespace ServiceBase
{
    public class Program
    {
       
        private static IConfiguration Configuration { get; set; }
        
        public static async Task Main(string[] args)
        {

            //write any exceptions on startup to the log.
            try
            {
                var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();
            
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(Configuration)
                    .CreateLogger();
                
                var host = CreateWebHostBuilder(args).Build();
                //run startup task.
                await host.RunTasksAsync();
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            
            //integration tests don't call program.main
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            
            return new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(x=> x.AddConfiguration(Configuration))
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseSerilog();
        }
    }
}