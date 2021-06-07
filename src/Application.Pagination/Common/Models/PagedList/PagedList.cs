using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Pagination.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Pagination.Common.Models.PagedList
{
    public class PagedList<T> : IPagedList<T>
    {
        #region Constructors

        /// <summary>
        /// Creates an instance of type <see cref="PagedList{T}"/>.
        /// To create it outside of this class, use one of static factory methods.
        /// </summary>
        private PagedList()
        {
        }

        #endregion

        #region Properties

        public int TotalPagesCount { get; set; }
        public int TotalItemsCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int CurrentPageNumber { get; set; }
        public IEnumerable<T> CurrentPageItems { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// A static factory method, that paginates the given <paramref name="query"/>
        /// according to <paramref name="paginationRequest"/> and creates an instance of type <see cref="PagedList{T}"/>.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="paginationRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateFromQueryAsync(IQueryable<T> query,
            IPaginationRequest paginationRequest,
            CancellationToken cancellationToken = default)
        {
            List<T> currentPageItems = await query
                .Paginate(paginationRequest)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            int totalItemsCount = await query.CountAsync(cancellationToken)
                .ConfigureAwait(false);

            int totalPagesCount = GetTotalPagesCount(totalItemsCount, paginationRequest.PageSize);

            return new()
            {
                TotalItemsCount = totalItemsCount,
                TotalPagesCount = totalPagesCount,
                HasPreviousPage = paginationRequest.PageNumber > 1,
                HasNextPage = paginationRequest.PageNumber < totalPagesCount,
                CurrentPageNumber = paginationRequest.PageNumber,
                CurrentPageItems = currentPageItems
            };
        }

        /// <summary>
        /// A static factory method, that creates an instance of type <see cref="PagedList{T}"/>,
        /// based on the given parameters.
        /// </summary>
        /// <param name="pageItems"></param>
        /// <param name="totalItemsCount"></param>
        /// <param name="paginationRequest"></param>
        /// <returns></returns>
        public static PagedList<T> CreateFromExistingPage(IEnumerable<T> pageItems, int totalItemsCount,
            IPaginationRequest paginationRequest)
        {
            int totalPagesCount = GetTotalPagesCount(totalItemsCount, paginationRequest.PageSize);

            return new()
            {
                TotalItemsCount = totalItemsCount,
                TotalPagesCount = totalPagesCount,
                HasPreviousPage = paginationRequest.PageNumber > 1,
                HasNextPage = paginationRequest.PageNumber < totalPagesCount,
                CurrentPageNumber = paginationRequest.PageNumber,
                CurrentPageItems = pageItems
            };
        }

        /// <summary>
        /// A static factory method, that creates an instance of type <see cref="PagedList{T}"/>,
        /// containing no items inside.
        /// </summary>
        public static PagedList<T> CreateEmptyPagedList(IPaginationRequest paginationRequest)
        {
            return CreateFromExistingPage(Enumerable.Empty<T>(), 0, paginationRequest);
        }

        private static int GetTotalPagesCount(int totalItemsCount, int pageSize)
        {
            return (int) Math.Ceiling(totalItemsCount / (double) pageSize);
        }

        #endregion
    }
}