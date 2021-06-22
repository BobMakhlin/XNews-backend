using Presentation.API.Controllers;

namespace Presentation.API.Requests.ControllerRequests
{
    /// <summary>
    /// Contains types that represent requests for action-methods of <see cref="UsersController"/>.
    /// </summary>
    public static class UsersControllerRequests
    {
        /// <summary>
        /// Represents a request for <see cref="UsersController.ChangeUserPasswordAsync"/> action-method.
        /// </summary>
        public class ChangeUserPasswordRequest
        {
            public string CurrentPassword { get; set; }
            public string NewPassword { get; set; }
        }
    }
}