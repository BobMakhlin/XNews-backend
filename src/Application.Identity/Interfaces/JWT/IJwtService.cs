using System.Threading.Tasks;
using Application.Identity.Models;
using Application.Identity.Results;

namespace Application.Identity.Interfaces.JWT
{
    /// <summary>
    /// A service, that contains the functionality to work with JWT-tokens.
    /// </summary>
    public interface IJwtService<in TUser, in TPassword, in TRefreshToken>
    {
        /// <summary>
        /// Authenticates the <paramref name="user"/> with the specified <paramref name="password"/>.
        /// </summary>
        /// <returns>
        /// <para>
        ///     Method returns the following tuple:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Object, containing info about the identity operation execution.</description>
        ///         </item>
        ///         <item>
        ///             <description>Object, containing JWT-tokens (the result of the authentication).</description>
        ///         </item>
        ///     </list>
        /// </para>
        /// <para>
        ///     If the specified <paramref name="password"/> is incorrect relatively to the <paramref name="user"/>,
        ///     the method returns the following tuple:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>The object, that determines, that the operation failed.</description>
        ///         </item>
        ///         <item>
        ///             <description><see langword="null"/></description>
        ///         </item>
        ///     </list>
        /// </para>
        /// <para>
        ///     If the specified <paramref name="password"/> is correct relatively to the <paramref name="user"/>,
        ///     the method returns the following tuple:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>The object, that determines, that the operation succeeded.</description>
        ///         </item>
        ///         <item>
        ///             <description>The object, that contains JWT-tokens.</description>
        ///         </item>
        ///     </list>
        /// </para>
        /// </returns>
        Task<(IIdentityResult, AuthenticationResponse)> AuthenticateAsync(TUser user, TPassword password);

        /// <summary>
        /// Checks the specified <paramref name="refreshToken"/> and if it's valid, returns a JWT-tokens pair.
        /// </summary>
        /// <returns>
        /// <para>
        ///     Method returns the following tuple:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>Object, containing info about the identity operation execution.</description>
        ///         </item>
        ///         <item>
        ///             <description>Object, containing JWT-tokens.</description>
        ///         </item>
        ///     </list>
        /// </para>
        /// <para>
        ///     If the specified <paramref name="refreshToken"/> is not valid,
        ///     the method returns the following tuple:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>The object, that determines, that the operation failed.</description>
        ///         </item>
        ///         <item>
        ///             <description><see langword="null"/></description>
        ///         </item>
        ///     </list>
        /// </para>
        /// <para>
        ///     If the specified <paramref name="refreshToken"/> is valid,
        ///     the method returns the following tuple:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>The object, that determines, that the operation succeeded.</description>
        ///         </item>
        ///         <item>
        ///             <description>The object, that contains JWT-tokens.</description>
        ///         </item>
        ///     </list>
        /// </para>
        /// </returns>
        Task<(IIdentityResult, AuthenticationResponse)> RefreshSessionAsync(TRefreshToken refreshToken);
    }
}