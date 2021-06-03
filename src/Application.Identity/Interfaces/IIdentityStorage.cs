using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Results;

namespace Application.Identity.Interfaces
{
    /// <summary>
    /// Represents a simple CRUD-storage of items of type <see cref="TItem"/>.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of the item, stored inside of a storage.
    /// </typeparam>
    public interface IIdentityStorage<TItem>
    {
        /// <summary>
        /// Queries all items of the storage.
        /// </summary>
        IQueryable<TItem> GetAll();
        /// <summary>
        /// Adds the specified <see cref="item"/> into a storage.
        /// </summary>
        Task<IIdentityResult> CreateAsync(TItem item);
        /// <summary>
        /// Update the specified <see cref="item"/> inside of a storage.
        /// </summary>
        Task<IIdentityResult> UpdateAsync(TItem item);
        /// <summary>
        /// Deletes the specified <see cref="item"/> from a storage.
        /// </summary>
        Task<IIdentityResult> DeleteAsync(TItem item);
    }
}