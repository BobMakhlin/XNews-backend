using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Pagination.Extensions;
using Application.Pagination.Options;
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
            ThrowIfPaginationRequestIsInvalid(paginationRequest);

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
            ThrowIfPaginationRequestIsInvalid(paginationRequest);
            
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
        /// If <paramref name="request"/> is invalid, throws an exception.
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If property <see cref="IPaginationRequest.PageNumber"/> is less than 1.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If property <see cref="IPaginationRequest.PageSize"/> is less than 1.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// If property <see cref="IPaginationRequest.PageSize"/> is more than maximum allowed size.
        /// </exception>
        private static void ThrowIfPaginationRequestIsInvalid(IPaginationRequest request)
        {
            if (request.PageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(request),
                    $"{nameof(request.PageNumber)} property is less than 1");
            }

            if (request.PageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(request),
                    $"{nameof(request.PageSize)} property is less than 1");
            }

            int maxPageSize = PaginationOptions.MaxPageSize;
            if (request.PageSize > maxPageSize)
            {
                string message = $"The requested page size is {request.PageSize}, " +
                                 $"while maximum page size is {maxPageSize}";
                throw new ArgumentOutOfRangeException(nameof(request), message);
            }
        }

        private static int GetTotalPagesCount(int totalItemsCount, int pageSize)
        {
            return (int) Math.Ceiling(totalItemsCount / (double) pageSize);
        }

        #endregion
    }
}