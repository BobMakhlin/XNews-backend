using Application.Identity.Entities;
using Application.Identity.Interfaces;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Models.JWT;
using Infrastructure.Identity.DataAccess;
using Infrastructure.Identity.Helpers;
using Infrastructure.Identity.Options;
using Infrastructure.Identity.Services;
using Infrastructure.Identity.Services.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity
{
    public static class DependencyInjection
    {
        #region Fields

        /// <summary>
        /// Configuration key by which the access token is stored in the configuration.
        /// </summary>
        private static string _jwtAccessTokenSectionConfigurationKey = "JWT:AccessToken";

        /// <summary>
        /// Algorithm used for JWT access token encryption.
        /// </summary>
        private static string _jwtAccessTokenAlgorithm = SecurityAlgorithms.HmacSha256;

        #endregion

        #region Methods

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
        public static IServiceCollection AddIdentitySystem(this IServiceCollection services,
            IConfiguration configuration)
        {
            AddIdentity(services);
            AddIdentityCommonServices(services);
            AddAuthenticationServices(services, configuration);
            AddJwtBearerAuthentication(services);

            return services;
        }

        /// <summary>
        /// Registers the ASP.NET Core identity in the specified <paramref name="serviceCollection"/>.
        /// </summary>
        private static void AddIdentity(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<XNewsIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Registers the common identity services in the specified <paramref name="serviceCollection"/>.
        /// </summary>
        private static void AddIdentityCommonServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IIdentityStorage<ApplicationUser, string>, ApplicationUserStorage>();
            serviceCollection.AddScoped<IIdentityStorage<ApplicationRole, string>, ApplicationRoleStorage>();
            serviceCollection
                .AddScoped<IUserPasswordService<ApplicationUser, string>, ApplicationUserPasswordService>();
            serviceCollection.AddScoped<IUserRoleService<ApplicationUser, ApplicationRole>, UserRoleService>();
        }

        /// <summary>
        /// Registers authentication services in the specified <paramref name="serviceCollection"/>.
        /// </summary>
        private static void AddAuthenticationServices(IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.Configure<JwtAccessTokenConfig>
            (
                configuration.GetSection(_jwtAccessTokenSectionConfigurationKey)
            );
            serviceCollection.Configure<JwtAccessTokenConfig>(opts =>
            {
                opts.EncryptionAlgorithm = _jwtAccessTokenAlgorithm;
            });

            serviceCollection.AddScoped<IJwtAccessTokenGenerator<ApplicationUser, string>, JwtAccessTokenGenerator>();
            serviceCollection.AddScoped<IJwtService<ApplicationUser, string>, JwtService>();
        }

        /// <summary>
        /// Enables the JWT bearer authentication by registering it in the specified <see cref="serviceCollection"/>.
        /// </summary>
        private static void AddJwtBearerAuthentication(IServiceCollection serviceCollection)
        {
            using ServiceProvider provider = serviceCollection.BuildServiceProvider();
            JwtAccessTokenConfig tokenConfig = provider
                .GetRequiredService<IOptions<JwtAccessTokenConfig>>()
                .Value;

            serviceCollection
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,

                        ClockSkew = tokenConfig.ClockSkew,
                        ValidIssuer = tokenConfig.ValidIssuer,
                        ValidAudience = tokenConfig.ValidAudience,
                        IssuerSigningKey =
                            SymmetricSecurityKeyHelper.CreateFromString(tokenConfig.IssuerSigningKey)
                    };
                });
        }

        #endregion
    }
}