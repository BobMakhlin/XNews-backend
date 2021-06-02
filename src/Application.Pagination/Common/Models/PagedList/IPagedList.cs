using System.Collections.Generic;

namespace Application.Pagination.Common.Models.PagedList
{
    /// <summary>
    /// Represents a subset of a collection of objects and contains information about superset.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T>
    {
        public int TotalPagesCount { get; set; }
        public int TotalItemsCount { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int CurrentPageNumber { get; set; }
        public IEnumerable<T> CurrentPageItems { get; set; }
    }
}