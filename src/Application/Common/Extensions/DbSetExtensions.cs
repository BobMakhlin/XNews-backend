using System.Threading.Tasks;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="DbSet{TEntity}"/>.
    /// </summary>
    public static class DbSetExtensions
    {
        /// <summary>
        /// Throws an exception of type <see cref="NotFoundException"/> if the item
        /// with the specified <paramref name="primaryKeyValues"/> doesn't exist.
        /// </summary>
        public static async Task ThrowIfDoesNotExistAsync<TEntity>(this DbSet<TEntity> dbSet,
            params object[] primaryKeyValues)
            where TEntity : class
        {
            TEntity entity = await dbSet.FindAsync(primaryKeyValues)
                .ConfigureAwait(false);
            
            if (entity == null)
            {
                throw new NotFoundException(typeof(TEntity).Name, primaryKeyValues);
            }
        }
    }
}