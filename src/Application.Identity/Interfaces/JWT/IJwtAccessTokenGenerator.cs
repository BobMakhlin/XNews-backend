using System.Threading.Tasks;

namespace Application.Identity.Interfaces.JWT
{
    /// <summary>
    /// Represents a generator of JWT access token.
    /// </summary>
    public interface IJwtAccessTokenGenerator<in TUser, TAccessToken>
    {
        /// <summary>
        /// Creates a JWT access token for the specified <paramref name="user"/>.
        /// </summary>
        Task<TAccessToken> GenerateAsync(TUser user);
    }
}