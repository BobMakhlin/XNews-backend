using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Presentation.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder
                .ConfigureNLog("nlog.config")
                .GetCurrentClassLogger();
            
            try
            {
                logger.Debug("Init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                
                .ConfigureAppConfiguration(InitializeConfiguration)
                
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();

        
        /// <summary>
        /// Initializes the configuration of the application using the given parameters.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configBuilder"></param>
        private static void InitializeConfiguration(HostBuilderContext context, IConfigurationBuilder configBuilder)
        {
            string environmentName = context.HostingEnvironment.EnvironmentName;

            configBuilder
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddJsonFile("appsettings.Persistence.json", false, true)
                .AddJsonFile("appsettings.CORS.json", false, true)
                .AddJsonFile("appsettings.JWT.json", false, true);

            configBuilder
                .AddEnvironmentVariables();

            configBuilder
                .AddUserSecrets(Assembly.GetExecutingAssembly(), false);
        }
    }
}