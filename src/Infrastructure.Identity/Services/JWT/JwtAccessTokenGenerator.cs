using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Identity.Entities;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Models.JWT;
using Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity.Services.JWT
{
    public class JwtAccessTokenGenerator : IJwtAccessTokenGenerator<ApplicationUser, string>
    {
        #region Fields

        /// <summary>
        /// Algorithm that is used for signing credentials of the JWT-access token.
        /// </summary>
        private readonly string _signingCredentialsAlgorithm = SecurityAlgorithms.HmacSha256;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtAccessTokenConfig _tokenConfig;

        #endregion

        #region Constructors

        public JwtAccessTokenGenerator(UserManager<ApplicationUser> userManager,
            IOptions<JwtAccessTokenConfig> tokenConfig)
        {
            _userManager = userManager;
            _tokenConfig = tokenConfig.Value;
        }

        #endregion

        #region IJwtAccessTokenGenerator<ApplicationUser>

        public async Task<string> GenerateAsync(ApplicationUser user)
        {
            IEnumerable<Claim> claims = await GetClaimsForJwtTokenAsync(user)
                .ConfigureAwait(false);
            SigningCredentials signingCredentials = GetSigningCredentials();

            JwtSecurityToken jwtTokenConfig = CreateJwtSecurityToken(claims, signingCredentials, _tokenConfig);

            var jwtTokenWriter = new JwtSecurityTokenHandler();
            return jwtTokenWriter.WriteToken(jwtTokenConfig);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of type <see cref="JwtSecurityToken"/> based on the specified parameters.
        /// </summary>
        private JwtSecurityToken CreateJwtSecurityToken(IEnumerable<Claim> claims,
            SigningCredentials signingCredentials,
            JwtAccessTokenConfig tokenConfig)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime expirationDateTime = currentDateTime.Add(tokenConfig.Lifetime);

            return new JwtSecurityToken
            (
                tokenConfig.ValidIssuer,
                tokenConfig.ValidAudience,
                claims,
                currentDateTime,
                expirationDateTime,
                signingCredentials
            );
        }

        /// <summary>
        /// Returns a signing credentials, that can be used to sign the JWT-key.
        /// </summary>
        private SigningCredentials GetSigningCredentials()
        {
            SymmetricSecurityKey symmetricSecurityKey =
                SymmetricSecurityKeyHelper.CreateFromString(_tokenConfig.IssuerSigningKey);

            return new SigningCredentials(symmetricSecurityKey, _signingCredentialsAlgorithm);
        }

        /// <summary>
        /// Returns a collection of JWT-token claims for the specified <paramref name="user"/>.
        /// This collection contains claims belonging to the <paramref name="user"/>,
        /// a claim for each role of this <paramref name="user"/> and some default roles.
        /// </summary>
        private async Task<IEnumerable<Claim>> GetClaimsForJwtTokenAsync(ApplicationUser user)
        {
            IEnumerable<Claim> userClaims = await _userManager.GetClaimsAsync(user)
                .ConfigureAwait(false);
            IEnumerable<Claim> roleClaims = await GetClaimsOfUserRolesAsync(user)
                .ConfigureAwait(false);
            IEnumerable<Claim> defaultClaims = GetDefaultJwtTokenClaims(user);

            return defaultClaims
                .Union(userClaims)
                .Union(roleClaims);
        }

        /// <summary>
        /// Returns a collection of claims, containing a claim of type <see cref="ClaimsIdentity.DefaultRoleClaimType"/>
        /// for each role of the specified <paramref name="user"/>.
        /// </summary>
        private async Task<IEnumerable<Claim>> GetClaimsOfUserRolesAsync(ApplicationUser user)
        {
            IEnumerable<string> roles = await _userManager.GetRolesAsync(user)
                .ConfigureAwait(false);
            return roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r));
        }

        /// <summary>
        /// Returns default JWT-token claims for the specified <paramref name="user"/>.
        /// </summary>
        private IEnumerable<Claim> GetDefaultJwtTokenClaims(ApplicationUser user)
        {
            return new[]
            {
                new Claim(JwtCustomClaimNames.UserId, user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }

        #endregion
    }
}