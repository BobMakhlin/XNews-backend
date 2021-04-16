using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace Application.Common.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="IQueryable"/> that allow to work easier with AutoMapper.
    /// </summary>
    public static class QueryableMapperExtensions
    {
        /// <summary>
        /// Converts the given <paramref name="query"/> to type <see cref="List{TDestination}"/>.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="provider">Mapper configuration</param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        public static async Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable query,
            IConfigurationProvider provider, CancellationToken cancellationToken = default)
        {
            return await query
                .ProjectTo<TDestination>(provider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Converts the given <paramref name="query"/> to type <see cref="IQueryable{TDestination}"/>
        /// and calls <see cref="o:EntityFrameworkQueryableExtensions.SingleOrDefaultAsync"/> on it.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="provider"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        public static async Task<TDestination> ProjectToSingleOrDefaultAsync<TDestination>(this IQueryable query,
            IConfigurationProvider provider, CancellationToken cancellationToken = default)
        {
            return await query
                .ProjectTo<TDestination>(provider)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}