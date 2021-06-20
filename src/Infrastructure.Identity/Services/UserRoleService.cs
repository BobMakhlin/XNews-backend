using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Identity.Results;
using Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    public class UserRoleService : IUserRoleService<ApplicationUser, ApplicationRole>
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        #endregion

        #region Constructors

        public UserRoleService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #endregion

        #region IUserRoleService<ApplicationUser, ApplicationRole>

        public IQueryable<ApplicationRole> GetUserRoles(ApplicationUser user)
        {
            return _roleManager.Roles
                .Where
                (
                    role => role.UserRoles.Any(userRole => userRole.UserId == user.Id)
                );
        }

        public IQueryable<ApplicationUser> GetRoleUsers(ApplicationRole role)
        {
            return _userManager.Users
                .Where
                (
                    user => user.UserRoles.Any(userRole => userRole.RoleId == role.Id)
                );
        }

        public async Task<IIdentityResult> AddUserToRoleAsync(ApplicationUser user, ApplicationRole role)
        {
            IdentityResult identityResult = await _userManager.AddToRoleAsync(user, role.Name)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, ApplicationRole role)
        {
            IdentityResult identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<bool> IsUserInRoleAsync(ApplicationUser user, ApplicationRole role)
        {
            return await _userManager.IsInRoleAsync(user, role.Name)
                .ConfigureAwait(false);
        }

        #endregion
    }
}