using System.Reflection;
using Application.Common.Behaviours;
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
            AddMediatorPipelineBehaviours(services);
            
            return services;
        }

        /// <summary>
        /// Registers MediatR pipeline behaviours on the given <paramref name="services"/>.
        /// </summary>
        /// <param name="services"></param>
        private static void AddMediatorPipelineBehaviours(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionLoggingBehaviour<,>));
        }
    }
}