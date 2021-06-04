using Application.Identity.Interfaces;
using Application.Identity.Models;
using Infrastructure.Identity.DataAccess;
using Infrastructure.Identity.Options;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers the DbContext, used to store identity tables.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString(InfrastructureIdentityOptions.IdentityDatabase);
            services.AddDbContext<XNewsIdentityDbContext>
            (
                options => options.UseSqlServer(connectionString)
            );

            return services;
        }

        /// <summary>
        /// Registers the identity system on the specified <paramref name="services"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentitySystem(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<XNewsIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityStorage<ApplicationUser>, ApplicationUserStorage>();
            services.AddScoped<IIdentityStorage<ApplicationRole>, ApplicationRoleStorage>();
            services.AddScoped<IUserPasswordService<ApplicationUser, string>, ApplicationUserPasswordService>();
            services.AddScoped<IUserRoleService<ApplicationUser, ApplicationRole>, UserRoleService>();

            return services;
        }
    }
}