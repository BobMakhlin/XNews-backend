using System.Reflection;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validation
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers validation of commands and queries.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationValidation(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            services.AddFluentValidation(new[] {currentAssembly});

            return services;
        }
    }
}