using Application.Common.Exceptions;
using Application.Identity.Results;

namespace Application.Common.Extensions
{
    /// <summary>
    /// Contains extension method for type <see cref="IIdentityResult"/>.
    /// </summary>
    public static class IIdentityResultExtensions
    {
        /// <summary>
        /// Throws an exception, if the specified <paramref name="identityResult"/> represents a failed operation result.
        /// </summary>
        /// <exception cref="IIdentityResultExtensions"></exception>
        public static void ThrowIfFailed(this IIdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                throw new IdentityException(identityResult.Errors);
            }
        }
    }
}