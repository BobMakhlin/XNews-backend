using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Results;

namespace Application.Identity.Interfaces
{
    /// <summary>
    /// Represents a storage, that allows to interact with roles and users.
    /// </summary>
    public interface IUserRoleService<in TUser, TRole>
    {
        /// <summary>
        /// Returns a collection of roles of the specified <paramref name="user"/>.
        /// </summary>
        IQueryable<TRole> GetUserRoles(TUser user);

        /// <summary>
        /// Adds the specified <paramref name="role"/> to a <paramref name="user"/>.
        /// </summary>
        Task<IIdentityResult> AddUserToRoleAsync(TUser user, TRole role);
 
        /// <summary>
        /// Removes the specified <paramref name="role"/> from a <paramref name="user"/>.
        /// </summary>
        Task<IIdentityResult> RemoveUserFromRoleAsync(TUser user, TRole role);

        /// <summary>
        /// Determines whether the specified <paramref name="user"/> has the specified <paramref name="role"/>.
        /// </summary>
        Task<bool> IsUserInRoleAsync(TUser user, TRole role);
    }
}