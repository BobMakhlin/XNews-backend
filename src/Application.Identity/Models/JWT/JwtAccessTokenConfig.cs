using System;

namespace Application.Identity.Models.JWT
{
    /// <summary>
    /// Represents a configuration of JWT access token.
    /// </summary>
    public class JwtAccessTokenConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }
        public TimeSpan ClockSkew { get; set; }
        public TimeSpan Lifetime { get; set; }
    }
}