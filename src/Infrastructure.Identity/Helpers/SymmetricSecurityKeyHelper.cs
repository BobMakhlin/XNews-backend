using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Helpers
{
    /// <summary>
    /// A helper class for <see cref="SymmetricSecurityKey"/>.
    /// </summary>
    public static class SymmetricSecurityKeyHelper
    {
        /// <summary>
        /// Creates an object of type <see cref="SymmetricSecurityKey"/> based on the given string.
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey CreateFromString(string keyString)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(keyString);
            return new SymmetricSecurityKey(keyBytes);
        }
    }
}