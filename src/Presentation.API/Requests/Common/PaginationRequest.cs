namespace Presentation.API.Requests.Common
{
    /// <summary>
    /// Represents a pagination-request for controller action-methods.
    /// </summary>
    public class PaginationRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}