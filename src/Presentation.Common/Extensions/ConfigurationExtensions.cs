using Microsoft.Extensions.Configuration;

namespace Presentation.Common.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Finds the value with the specified <paramref name="key"/> and splits it by <paramref name="separator"/>.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <param name="separator">String used to split items of an array, stored as a value inside of config-file</param>
        /// <returns></returns>
        public static string[] GetArray(this IConfiguration config, string key, string separator)
        {
            return config[key].Split(separator);
        }
    }
}