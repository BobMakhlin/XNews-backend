using System.Reflection;
using Application.Identity.Entities;
using Application.Identity.Interfaces;
using Application.Identity.Interfaces.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.DataAccess
{
    internal class XNewsIdentityDbContext
        :
            IdentityDbContext
            <
                ApplicationUser,
                ApplicationRole,
                string,
                IdentityUserClaim<string>,
                ApplicationUserRole,
                IdentityUserLogin<string>,
                IdentityRoleClaim<string>,
                IdentityUserToken<string>
            >,
            IXNewsIdentityDbContextSimplified
    {
        #region Constructors

        public XNewsIdentityDbContext(DbContextOptions<XNewsIdentityDbContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}