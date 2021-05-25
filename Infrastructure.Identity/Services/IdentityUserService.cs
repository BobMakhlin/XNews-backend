using System.Threading.Tasks;
using Application.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    /// <summary>
    /// An implementation of interface <see cref="IIdentityUserService{TUser}"/>,
    /// working with <see cref="UserManager{TUser}"/> inside.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class IdentityUserService<TUser> : IIdentityUserService<TUser> where TUser : IdentityUser
    {
        #region Fields

        private readonly UserManager<TUser> _userManager;

        #endregion

        #region Constructors

        public IdentityUserService(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        #region IIdentityUserService<TUser>

        public async Task<TUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId)
                .ConfigureAwait(false);
        }

        #endregion
    }
}