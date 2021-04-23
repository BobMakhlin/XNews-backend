namespace Application.Pagination.Options
{
    public static class PaginationOptions
    {
        public static readonly int DefaultPageNumber = 1;
        public static readonly int DefaultPageSize = 10;
        /// <summary>
        /// Maximum count of items per one page.
        /// </summary>
        public static readonly int MaxPageSize = 20;
    }
}