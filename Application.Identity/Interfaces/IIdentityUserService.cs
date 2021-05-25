using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Identity.Interfaces
{
    /// <summary>
    /// Provides API to interact with the user storage.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IIdentityUserService<TUser> where TUser : IdentityUser
    {
        /// <summary>
        /// Finds the user with the specified <paramref name="userId"/>
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<TUser> FindByIdAsync(string userId);
    }
}