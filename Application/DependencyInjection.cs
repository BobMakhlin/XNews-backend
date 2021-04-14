using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application-layer services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(currentAssembly);
            services.AddMediatR(currentAssembly);
            
            return services;
        }
    }
}