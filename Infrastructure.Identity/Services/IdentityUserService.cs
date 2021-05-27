using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Services
{
    /// <summary>
    /// An implementation of interface <see cref="IIdentityUserService{ApplicationUser, ApplicationRole}"/>,
    /// working with <see cref="UserManager{ApplicationUser}"/> inside.
    /// </summary>
    public class IdentityUserService : IIdentityUserService<ApplicationUser, ApplicationRole>
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructors

        public IdentityUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        #region IIdentityUserService<ApplicationUser, ApplicationRole>

        public IQueryable<ApplicationUser> Users => _userManager.Users;

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId)
                .ConfigureAwait(false);
        }

        #endregion
    }
}