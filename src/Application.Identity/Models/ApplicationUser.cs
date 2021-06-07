using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}