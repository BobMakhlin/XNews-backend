using System;

namespace Application.Identity.Models.JWT
{
    /// <summary>
    /// Represents a configuration of JWT refresh token.
    /// </summary>
    public class JwtRefreshTokenConfig
    {
        public TimeSpan Lifetime { get; set; }
        public int SymbolsCount { get; set; }
    }
}