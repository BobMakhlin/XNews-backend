using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Identity.Results;
using Infrastructure.Identity.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services
{
    /// <summary>
    /// Represents a storage of items of type <see cref="ApplicationUser"/>.
    /// </summary>
    public class ApplicationUserStorage : IIdentityStorage<ApplicationUser, string>
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructors

        public ApplicationUserStorage(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion
        
        #region IIdentityStorage<ApplicationUser>

        public IQueryable<ApplicationUser> GetAll() => _userManager.Users;

        public async Task<IIdentityResult> CreateAsync(ApplicationUser item)
        {
            IdentityResult identityResult = await _userManager.CreateAsync(item)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> UpdateAsync(ApplicationUser item)
        {
            IdentityResult identityResult = await _userManager.UpdateAsync(item)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> DeleteAsync(ApplicationUser item)
        {
            IdentityResult identityResult = await _userManager.DeleteAsync(item)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id)
                .ConfigureAwait(false);
        }

        #endregion
    }
}