using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Results;

namespace Application.Identity.Interfaces.Storages
{
    /// <summary>
    /// Represents a simple CRUD-storage of items of type <see cref="TItem"/>.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of the item, stored inside of a storage.
    /// </typeparam>
    /// <typeparam name="TItemId">
    /// The type of the item identifier.
    /// </typeparam>
    public interface IIdentityStorage<TItem, in TItemId>
        where TItem : class
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
        /// <summary>
        /// Finds the item by <paramref name="id"/> in a storage.
        /// </summary>
        Task<TItem> FindByIdAsync(TItemId id);
    }
}