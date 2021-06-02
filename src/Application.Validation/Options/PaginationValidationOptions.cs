namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate the pagination requests.
    /// </summary>
    internal static class PaginationValidationOptions
    {
        /// <summary>
        /// Determines the minimum page number.
        /// </summary>
        public static readonly int MinPageNumber = 1;
        /// <summary>
        /// Determines the minimum size of a page.
        /// </summary>
        public static readonly int MinPageSize = 1;
        /// <summary>
        /// Determines the maximum size of a page.
        /// </summary>
        public static readonly int MaxPageSize = 20;
    }
}