using System;
using System.Security.Cryptography;

namespace Infrastructure.Identity.Helpers
{
    /// <summary>
    /// Contains methods used to generate random values.
    /// </summary>
    public static class RandomGeneratorHelper
    {
        /// <summary>
        /// Creates a random string (string with random characters) with the specified <paramref name="length"/>.
        /// </summary>
        public static string GetRandomString(int length)
        {
            using var randomNumberGenerator = new RNGCryptoServiceProvider();
            
            var bitCount = length * 6;
            var byteCount = (bitCount + 7) / 8;

            var randomBytes = new byte[byteCount];
            randomNumberGenerator.GetBytes(randomBytes);
            
            return Convert.ToBase64String(randomBytes);
        }
    }
}