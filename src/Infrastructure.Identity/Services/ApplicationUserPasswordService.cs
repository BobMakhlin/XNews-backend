using System.Threading.Tasks;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Identity.Results;
using Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    public class ApplicationUserPasswordService : IUserPasswordService<ApplicationUser, string>
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructors

        public ApplicationUserPasswordService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        #region IUserPasswordService<ApplicationUser, string>

        public async Task<IIdentityResult> CreateUserWithPasswordAsync(ApplicationUser user, string password)
        {
            IdentityResult identityResult = await _userManager.CreateAsync(user, password)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> ChangeUserPasswordAsync(ApplicationUser user, string currentPassword,
            string newPassword)
        {
            IdentityResult identityResult = await _userManager
                .ChangePasswordAsync(user, currentPassword, newPassword)
                .ConfigureAwait(false);

            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> AddPasswordToUserAsync(ApplicationUser user, string password)
        {
            IdentityResult identityResult = await _userManager.AddPasswordAsync(user, password)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        #endregion
    }
}