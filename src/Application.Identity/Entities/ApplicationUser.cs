using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}