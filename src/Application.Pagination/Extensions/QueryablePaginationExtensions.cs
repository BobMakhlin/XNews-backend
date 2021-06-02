using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;

namespace Application.Pagination.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="IQueryable{T}"/> that allow to paginate your query.
    /// </summary>
    public static class QueryablePaginationExtensions
    {
        /// <summary>
        /// Paginates the <paramref name="query"/> according to <paramref name="paginationRequest"/>.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="paginationRequest"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, IPaginationRequest paginationRequest)
        {
            int countToSkip = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            int countToTake = paginationRequest.PageSize;

            return query
                .Skip(countToSkip)
                .Take(countToTake);
        }

        /// <summary>
        /// Creates an object of type <see cref="IPagedList{T}"/> based on <paramref name="query"/>
        /// according to <paramref name="paginationRequest"/>.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="paginationRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<IPagedList<T>> ProjectToPagedListAsync<T>(this IQueryable<T> query,
            IPaginationRequest paginationRequest, CancellationToken cancellationToken = default)
            => await PagedList<T>.CreateFromQueryAsync(query, paginationRequest, cancellationToken);
    }
}