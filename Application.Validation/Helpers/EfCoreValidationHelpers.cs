using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.Helpers
{
    public static class EfCoreValidationHelpers
    {
        /// <summary>
        /// Determines whether the specified <paramref name="newValue"/> is unique inside of
        /// the given <paramref name="dbSet"/>.
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="getColumnSelector">
        /// Determines the column, with which we will compare <paramref name="newValue"/>
        /// </param>
        /// <param name="newValue">
        /// Value, that will be checked for uniqueness
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TColumn"></typeparam>
        /// <returns></returns>
        public static async Task<bool> IsColumnUniqueInsideOfDbSetAsync<TEntity, TColumn>(DbSet<TEntity> dbSet,
            Expression<Func<TEntity, TColumn>> getColumnSelector,
            TColumn newValue,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            return !await dbSet
                .Select(getColumnSelector)
                .AnyAsync(column => column.Equals(newValue), cancellationToken);
        }
    }
}

