using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}