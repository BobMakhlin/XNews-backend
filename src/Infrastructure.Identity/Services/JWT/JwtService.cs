using System.Threading.Tasks;
using Application.Identity.Entities;
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

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtAccessTokenGenerator<ApplicationUser, string> _jwtAccessTokenGenerator;

        #endregion

        #region Constructors

        public JwtService(UserManager<ApplicationUser> userManager,
            IJwtAccessTokenGenerator<ApplicationUser, string> jwtAccessTokenGenerator)
        {
            _userManager = userManager;
            _jwtAccessTokenGenerator = jwtAccessTokenGenerator;
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

            string accessToken = await _jwtAccessTokenGenerator.GenerateAsync(user)
                .ConfigureAwait(false);

            return (CustomIdentityResult.Success(), new() {AccessToken = accessToken});
        }

        #endregion
    }
}