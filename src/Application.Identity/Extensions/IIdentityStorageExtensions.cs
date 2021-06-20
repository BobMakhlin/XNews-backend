using System.Threading.Tasks;
using Application.Identity.Interfaces.Storages;

namespace Application.Identity.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="IIdentityStorage{TItem,TItemId}"/>
    /// </summary>
    public static class IIdentityStorageExtensions
    {
        /// <summary>
        /// Checks if the <paramref name="storage"/> contains an item with the specified <paramref name="id"/>.
        /// </summary>
        public static async Task<bool> Exists<TItem, TItemId>
        (
            this IIdentityStorage<TItem, TItemId> storage,
            TItemId id
        )
            where TItem : class
        {
            TItem item = await storage.FindByIdAsync(id)
                .ConfigureAwait(false);
            return item != null;
        }
    }
}