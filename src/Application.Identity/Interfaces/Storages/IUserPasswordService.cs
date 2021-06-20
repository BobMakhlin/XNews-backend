using System.Threading.Tasks;
using Application.Identity.Results;

namespace Application.Identity.Interfaces.Storages
{
    /// <summary>
    /// Represents a storage, that allows to interact with passwords of users.
    /// </summary>
    public interface IUserPasswordService<in TUser, in TPassword>
    {
        /// <summary>
        /// Adds the specified <paramref name="user"/> with the specified <paramref name="password"/>
        /// to the storage.
        /// </summary>
        Task<IIdentityResult> CreateUserWithPasswordAsync(TUser user, TPassword password);

        /// <summary>
        /// Changes the password of the specified <paramref name="user"/> after confirming the specified
        /// <paramref name="currentPassword"/> is correct.
        /// </summary>
        Task<IIdentityResult> ChangeUserPasswordAsync(TUser user, TPassword currentPassword, TPassword newPassword);

        /// <summary>
        /// Adds the <paramref name="password"/> to the specified <paramref name="user"/>
        /// only if the user does not already have a password.
        /// </summary>
        Task<IIdentityResult> AddPasswordToUserAsync(TUser user, TPassword password);
    }
}