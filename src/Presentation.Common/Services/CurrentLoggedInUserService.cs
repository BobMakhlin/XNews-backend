using System.Security.Claims;
using Application.Identity.Claims;
using Application.Identity.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Presentation.Common.Services
{
    public class CurrentLoggedInUserService : ICurrentLoggedInUserService
    {
        #region Fields

        private readonly string _userIdClaim = JwtCustomClaimNames.UserId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructors

        public CurrentLoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region ICurrentLoggedInUserService

        public string GetUserId()
        {
            return User.FindFirstValue(_userIdClaim);
        }

        #endregion

        #region Methods

        private ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        #endregion
    }
}