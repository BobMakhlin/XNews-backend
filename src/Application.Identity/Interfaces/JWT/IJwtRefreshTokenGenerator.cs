using System.Threading.Tasks;

namespace Application.Identity.Interfaces.JWT
{
    /// <summary>
    /// Represents a generator of JWT refresh token.
    /// </summary>
    public interface IJwtRefreshTokenGenerator<TRefreshToken>
    {
        /// <summary>
        /// Creates a JWT refresh token.
        /// </summary>
        Task<TRefreshToken> GenerateAsync();
    }
}