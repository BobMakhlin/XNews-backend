using Application.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.DataAccess
{
    internal class XNewsIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> 
    {
        public XNewsIdentityDbContext(DbContextOptions<XNewsIdentityDbContext> options) : base(options)
        {
        }
    }
}