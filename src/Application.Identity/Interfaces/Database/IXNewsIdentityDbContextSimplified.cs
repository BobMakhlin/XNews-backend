using System.Threading;
using System.Threading.Tasks;
using Application.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Identity.Interfaces.Database
{
    /// <summary>
    /// An abstraction over the Identity DbContext, that doesn't allow to interact
    /// with users, roles, passwords, etc.
    /// </summary>
    public interface IXNewsIdentityDbContextSimplified
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}