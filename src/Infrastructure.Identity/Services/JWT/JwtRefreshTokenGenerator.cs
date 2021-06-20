using System;
using System.Threading.Tasks;
using Application.Identity.Entities;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Models.JWT;
using Infrastructure.Identity.Helpers;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Services.JWT
{
    public class JwtRefreshTokenGenerator : IJwtRefreshTokenGenerator<RefreshToken>
    {
        #region Fields

        private readonly JwtRefreshTokenConfig _tokenConfig;

        #endregion

        #region Constructors

        public JwtRefreshTokenGenerator(IOptions<JwtRefreshTokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig.Value;
        }

        #endregion

        #region IJwtRefreshTokenGenerator<RefreshToken>

        public Task<RefreshToken> GenerateAsync()
        {
            string randomString = RandomGeneratorHelper.GetRandomString(_tokenConfig.SymbolsCount);

            DateTime createdAt = DateTime.Now;
            DateTime expiresAt = createdAt.Add(_tokenConfig.Lifetime);

            RefreshToken refreshToken = new()
            {
                Token = randomString,
                CreatedAt = createdAt,
                ExpiresAt = expiresAt,
                RevokedAt = null
            };
            return Task.FromResult(refreshToken);
        }

        #endregion
    }
}