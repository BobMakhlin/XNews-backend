using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Presentation.Common.Extensions
{
    /// <summary>
    /// The <see cref="IApplicationBuilder"/> extensions to enable CORS-requests and
    /// build the CORS-policy using config-file.
    /// </summary>
    public static class CorsConfigurationExtensions
    {
        #region Fields

        private static string _allowAnyOriginKey = "CORS:AllowAnyOrigin";
        private static string _allowAnyHeaderKey = "CORS:AllowAnyHeader";
        private static string _allowAnyMethodKey = "CORS:AllowAnyMethod";
        private static string _allowedOriginsKey = "CORS:AllowedOrigins";
        private static string _allowedHeadersKey = "CORS:AllowedHeaders";
        private static string _allowedMethodsKey = "CORS:AllowedMethods";
        /// <summary>
        /// String used to split items of an array, stored as a value inside of config-file.
        /// </summary>
        private static string _configurationCollectionSeparator = ";";

        #endregion

        #region Methods

        /// <summary>
        /// Adds a CORS middleware to your web application pipeline to allow cross domain requests.
        /// CORS-policy will be built based on the specified <paramref name="config"/>.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> passed to your Configure method</param>
        /// <param name="config">Configuration used to create a CORS-policy.</param>
        /// <returns>The original <paramref name="app"/> parameter</returns>
        public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration config)
        {
            bool allowAnyOrigin = config.GetValue<bool>(_allowAnyOriginKey);
            bool allowAnyHeader = config.GetValue<bool>(_allowAnyHeaderKey);
            bool allowAnyMethod = config.GetValue<bool>(_allowAnyMethodKey);
            string[] allowedOrigins = config.GetArray(_allowedOriginsKey, _configurationCollectionSeparator);
            string[] allowedHeaders = config.GetArray(_allowedHeadersKey, _configurationCollectionSeparator);
            string[] allowedMethods = config.GetArray(_allowedMethodsKey, _configurationCollectionSeparator);

            app.UseCors(options =>
            {
                SetOrigins(options, allowAnyOrigin, allowedOrigins);
                SetHeaders(options, allowAnyHeader, allowedHeaders);
                SetMethods(options, allowAnyMethod, allowedMethods);
            });

            return app;
        }

        /// <summary>
        /// Adds the specified <paramref name="origins"/> using <paramref name="policyBuilder"/> when
        /// <paramref name="allowAnyOrigin"/> equals <see langword="false"/>.
        /// In another case, allows any origin using <paramref name="policyBuilder"/>.
        /// </summary>
        /// <param name="policyBuilder"></param>
        /// <param name="allowAnyOrigin"></param>
        /// <param name="origins"></param>
        private static void SetOrigins(CorsPolicyBuilder policyBuilder, bool allowAnyOrigin, string[] origins)
        {
            if (allowAnyOrigin)
            {
                policyBuilder.AllowAnyOrigin();
            }
            else
            {
                policyBuilder.WithOrigins(origins);
            }
        }

        /// <summary>
        /// Adds the specified <paramref name="headers"/> using <paramref name="policyBuilder"/> when
        /// <paramref name="allowAnyHeader"/> equals <see langword="false"/>.
        /// In another case, allows any header using <paramref name="policyBuilder"/>.
        /// </summary>
        /// <param name="policyBuilder"></param>
        /// <param name="allowAnyHeader"></param>
        /// <param name="headers"></param>
        private static void SetHeaders(CorsPolicyBuilder policyBuilder, bool allowAnyHeader, string[] headers)
        {
            if (allowAnyHeader)
            {
                policyBuilder.AllowAnyHeader();
            }
            else
            {
                policyBuilder.WithHeaders(headers);
            }
        }

        /// <summary>
        /// Adds the specified <paramref name="methods"/> using <paramref name="policyBuilder"/> when
        /// <paramref name="allowAnyMethod"/> equals <see langword="false"/>.
        /// In another case, allows any method using <paramref name="policyBuilder"/>.
        /// </summary>
        /// <param name="policyBuilder"></param>
        /// <param name="allowAnyMethod"></param>
        /// <param name="methods"></param>
        private static void SetMethods(CorsPolicyBuilder policyBuilder, bool allowAnyMethod, string[] methods)
        {
            if (allowAnyMethod)
            {
                policyBuilder.AllowAnyMethod();
            }
            else
            {
                policyBuilder.WithMethods(methods);
            }
        }
    }

    #endregion
}