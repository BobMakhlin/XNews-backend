using Application.Identity.Entities;
using Application.Identity.Interfaces.Database;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Interfaces.Storages;
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
        /// Configuration key by which the refresh token is stored in the configuration.
        /// </summary>
        private static string _jwtRefreshTokenSectionConfigurationKey = "JWT:RefreshToken";

        /// <summary>
        /// Configuration key of the identity user options section.
        /// </summary>
        private static string _identityUserOptionsSectionConfigurationKey = "Identity:User";

        /// <summary>
        /// Configuration key of the identity password options section.
        /// </summary>
        private static string _identityPasswordOptionsSectionConfigurationKey = "Identity:Password";

        /// <summary>
        /// Configuration key of the identity lockout options section.
        /// </summary>
        private static string _identityLockoutOptionsSectionConfigurationKey = "Identity:Lockout";

        /// <summary>
        /// Configuration key of the identity sign-in options section.
        /// </summary>
        private static string _identitySignInOptionsSectionConfigurationKey = "Identity:SignInOptions";

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

            services
                .AddScoped<IXNewsIdentityDbContextSimplified>(serviceProvider =>
                    serviceProvider.GetRequiredService<XNewsIdentityDbContext>());

            return services;
        }

        /// <summary>
        /// Registers the identity system on the specified <paramref name="services"/>.
        /// </summary>
        public static IServiceCollection AddIdentitySystem(this IServiceCollection services,
            IConfiguration configuration)
        {
            AddIdentity(services, configuration);
            AddIdentityCommonServices(services);
            AddAuthenticationServices(services, configuration);
            AddJwtBearerAuthentication(services);

            return services;
        }

        /// <summary>
        /// Registers the ASP.NET Core identity in the specified <paramref name="serviceCollection"/>.
        /// </summary>
        private static void AddIdentity(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.User = configuration.GetSection(_identityUserOptionsSectionConfigurationKey)
                        .Get<UserOptions>();
                    options.Password = configuration.GetSection(_identityPasswordOptionsSectionConfigurationKey)
                        .Get<PasswordOptions>();
                    options.Lockout = configuration.GetSection(_identityLockoutOptionsSectionConfigurationKey)
                        .Get<LockoutOptions>();
                    options.SignIn = configuration.GetSection(_identitySignInOptionsSectionConfigurationKey)
                        .Get<SignInOptions>();
                })
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
            AddJwtAccessTokenConfiguration(serviceCollection, configuration);
            AddJwtRefreshTokenConfiguration(serviceCollection, configuration);

            serviceCollection.AddScoped<IJwtAccessTokenGenerator<ApplicationUser, string>, JwtAccessTokenGenerator>();
            serviceCollection.AddScoped<IJwtRefreshTokenGenerator<RefreshToken>, JwtRefreshTokenGenerator>();
            serviceCollection.AddScoped<IJwtService<ApplicationUser, string, RefreshToken>, JwtService>();
        }

        /// <summary>
        /// Adds the configuration of JWT access token to the specified <paramref name="serviceCollection"/>.
        /// </summary>
        private static void AddJwtAccessTokenConfiguration(IServiceCollection serviceCollection,
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
        }

        /// <summary>
        /// Adds the configuration of JWT refresh token to the specified <paramref name="serviceCollection"/>.
        /// </summary>
        private static void AddJwtRefreshTokenConfiguration(IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            serviceCollection.Configure<JwtRefreshTokenConfig>
            (
                configuration.GetSection(_jwtRefreshTokenSectionConfigurationKey)
            );
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