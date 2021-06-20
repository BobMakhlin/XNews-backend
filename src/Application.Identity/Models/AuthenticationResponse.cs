namespace Application.Identity.Models
{
    /// <summary>
    /// Represents a response of authentication operation.
    /// </summary>
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}