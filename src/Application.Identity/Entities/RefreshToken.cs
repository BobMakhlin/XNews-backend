using System;

namespace Application.Identity.Entities
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        
        /// <summary>
        /// Checks if the token has expired.
        /// </summary>
        public bool IsExpired => DateTime.Now >= ExpiresAt;
        /// <summary>
        /// Checks if the token has been revoked.
        /// </summary>
        public bool IsRevoked => RevokedAt != null;
        /// <summary>
        /// Checks if the token if active.
        /// An active token is a token that hasn't expired and hasn't been revoked.
        /// </summary>
        public bool IsActive => !IsExpired && !IsRevoked;
    }
}