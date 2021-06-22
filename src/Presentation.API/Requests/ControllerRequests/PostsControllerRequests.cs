using Presentation.API.Controllers;

namespace Presentation.API.Requests.ControllerRequests
{
    /// <summary>
    /// Contains types that represent requests for action-methods of <see cref="PostsController"/>.
    /// </summary>
    public static class PostsControllerRequests
    {
        /// <summary>
        /// Represents a request for <see cref="PostsController.AddRateToPostAsync"/> action-method.
        /// </summary>
        public class AddRateToPostRequest
        {
            public string UserId { get; set; }
            public double Rate { get; set; }
        }
    }
}