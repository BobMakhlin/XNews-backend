namespace Application.Identity.Interfaces
{
    /// <summary>
    /// Allows to interact with current logged in user.
    /// </summary>
    public interface ICurrentLoggedInUserService
    {
        /// <summary>
        /// Returns the id of the current logged in user.
        /// </summary>
        public string GetUserId();
    }
}