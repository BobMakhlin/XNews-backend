using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Logging.DataAccess;
using Persistence.Logging.Options;

namespace Persistence.Logging
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers persistence-layer logging services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddPersistenceLogging(this IServiceCollection services, IConfiguration configuration)
        {
            AddLoggingDbContext(services, configuration);
            
            return services;
        }
        
        /// <summary>
        /// Registers logging <see cref="DbContext"/> inside of <paramref name="services"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void AddLoggingDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(PersistenceOptions.LoggingDatabase);

            services.AddDbContext<XNewsLoggingDbContext>
            (
                options => options.UseSqlServer(connectionString)
            );
        }
    }
}