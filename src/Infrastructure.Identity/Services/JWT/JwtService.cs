using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Entities;
using Application.Identity.Interfaces;
using Application.Identity.Interfaces.Database;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Models;
using Application.Identity.Results;
using Infrastructure.Identity.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services.JWT
{
    public class JwtService : IJwtService<ApplicationUser, string>
    {
        #region Fields

        private readonly IXNewsIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IJwtAccessTokenGenerator<ApplicationUser, string> _jwtAccessTokenGenerator;
        private readonly IJwtRefreshTokenGenerator<RefreshToken> _jwtRefreshTokenGenerator;

        #endregion

        #region Constructors

        public JwtService(UserManager<ApplicationUser> userManager,
            IJwtAccessTokenGenerator<ApplicationUser, string> jwtAccessTokenGenerator,
            IJwtRefreshTokenGenerator<RefreshToken> jwtRefreshTokenGenerator, IXNewsIdentityDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _jwtAccessTokenGenerator = jwtAccessTokenGenerator;
            _jwtRefreshTokenGenerator = jwtRefreshTokenGenerator;
        }

        #endregion

        #region IJwtService<ApplicationUser, string>

        public async Task<(IIdentityResult, AuthenticationResponse)> AuthenticateAsync(ApplicationUser user,
            string password)
        {
            bool passwordValid = await _userManager.CheckPasswordAsync(user, password)
                .ConfigureAwait(false);
            if (!passwordValid)
            {
                return (CustomIdentityResult.Failed("Password is not valid"), null);
            }

            string accessToken = await GetJwtAccessTokenForUserAsync(user)
                .ConfigureAwait(false);
            string refreshToken = await GetJwtRefreshTokenForUserAsync(user)
                .ConfigureAwait(false);

            return (CustomIdentityResult.Success(), new()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the JWT access token for the specified <paramref name="user"/>.
        /// </summary>
        private async Task<string> GetJwtAccessTokenForUserAsync(ApplicationUser user)
        {
            return await _jwtAccessTokenGenerator.GenerateAsync(user)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the JWT refresh token for the specified <paramref name="user"/>.
        /// </summary>
        /// <remarks>
        /// Returns the current active refresh token of the user, if the user has one.
        /// In another case it creates a new refresh token, attaches it to the user and returns this
        /// refresh token.
        /// </remarks>
        private async Task<string> GetJwtRefreshTokenForUserAsync(ApplicationUser user)
        {
            RefreshToken currentActiveRefreshToken = GetCurrentActiveRefreshTokenOfUser(user);
            if (currentActiveRefreshToken != null)
            {
                return currentActiveRefreshToken.Token;
            }

            RefreshToken newRefreshToken = await _jwtRefreshTokenGenerator.GenerateAsync()
                .ConfigureAwait(false);

            await AttachRefreshTokenToUserAsync(user, newRefreshToken)
                .ConfigureAwait(false);

            return newRefreshToken.Token;
        }

        /// <summary>
        /// Finds the current active refresh token of the specified <paramref name="user"/>.
        /// </summary>
        /// <remarks>
        /// It works with property <see cref="ApplicationUser.RefreshTokens"/> of the <paramref name="user"/> parameter.
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">
        /// When the user has more than one active refresh tokens.
        /// </exception>
        private RefreshToken GetCurrentActiveRefreshTokenOfUser(ApplicationUser user) =>
            user.RefreshTokens.SingleOrDefault(refreshToken => refreshToken.IsActive);

        /// <summary>
        /// Attaches the given <paramref name="refreshToken"/> to the specified <paramref name="user"/>
        /// and saves the changes in the database.
        /// </summary>
        private async Task AttachRefreshTokenToUserAsync(ApplicationUser user, RefreshToken refreshToken)
        {
            user.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        #endregion
    }
}