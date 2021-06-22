using Presentation.API.Controllers;

namespace Presentation.API.Requests.ControllerRequests
{
    /// <summary>
    /// Contains types that represent requests for action-methods of <see cref="CommentsController"/>.
    /// </summary>
    public static class CommentsControllerRequests
    {
        /// <summary>
        /// Represents a request for <see cref="CommentsController.AddRateToCommentAsync"/> action-method.
        /// </summary>
        public class AddRateToCommentRequest
        {
            public string UserId { get; set; }
            public double Rate { get; set; }
        }
    }
}