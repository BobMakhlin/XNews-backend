using System.Threading;
using System.Threading.Tasks;

namespace Application.Identity.Interfaces.Database
{
    public interface IXNewsIdentityDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}