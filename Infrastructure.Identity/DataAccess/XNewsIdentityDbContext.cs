using System.Reflection;
using Application.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.DataAccess
{
    internal class XNewsIdentityDbContext
        : IdentityDbContext
        <
            ApplicationUser, 
            ApplicationRole, 
            string, 
            IdentityUserClaim<string>,
            ApplicationUserRole, 
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>, 
            IdentityUserToken<string>
        >
    {
        public XNewsIdentityDbContext(DbContextOptions<XNewsIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}