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
    /// Represents a storage of items of type <see cref="ApplicationRole"/>.
    /// </summary>
    public class ApplicationRoleStorage : IIdentityStorage<ApplicationRole, string>
    {
        #region Fields

        private readonly RoleManager<ApplicationRole> _roleManager;

        #endregion

        #region Constructors

        public ApplicationRoleStorage(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion
        
        #region IIdentityStorage<ApplicationRole>

        public IQueryable<ApplicationRole> GetAll() => _roleManager.Roles;

        public async Task<IIdentityResult> CreateAsync(ApplicationRole item)
        {
            IdentityResult identityResult = await _roleManager.CreateAsync(item)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> UpdateAsync(ApplicationRole item)
        {
            IdentityResult identityResult = await _roleManager.UpdateAsync(item)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<IIdentityResult> DeleteAsync(ApplicationRole item)
        {
            IdentityResult identityResult = await _roleManager.DeleteAsync(item)
                .ConfigureAwait(false);
            return identityResult.ToIIdentityResult();
        }

        public async Task<ApplicationRole> FindByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id)
                .ConfigureAwait(false);
        }

        #endregion
    }
}