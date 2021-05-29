using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity.Interfaces
{
    /// <summary>
    /// Provides API to interact with user storage and user roles.
    /// </summary>
    public interface IIdentityUserService<TUser, TRole>
    {
        /// <summary>
        /// Returns a query to get all users.
        /// </summary>
        IQueryable<TUser> Users { get; }
        
        /// <summary>
        /// Finds the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TUser> FindByIdAsync(string userId);

        /// <summary>
        /// Finds roles of a user with the specified <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<TRole>> GetRolesOfUserAsync(string userId);
    }
}