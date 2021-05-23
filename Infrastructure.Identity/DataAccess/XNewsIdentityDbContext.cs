using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.DataAccess
{
    internal class XNewsIdentityDbContext : IdentityDbContext
    {
        public XNewsIdentityDbContext(DbContextOptions<XNewsIdentityDbContext> options) : base(options)
        {
        }
    }
}