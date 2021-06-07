using Application.Identity.Results;
using Infrastructure.Identity.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="IdentityResult"/>.
    /// </summary>
    public static class IdentityResultExtensions
    {
        /// <summary>
        /// Creates an object of type <see cref="IIdentityResult"/> based on the given <paramref name="identityResult"/>.
        /// </summary>
        /// <returns>The created object of type <see cref="IIdentityResult"/></returns>
        public static IIdentityResult ToIIdentityResult(this IdentityResult identityResult)
        {
            return new AspNetIdentityResultWrapper(identityResult);
        }
    }
}