using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Identity.Extensions;
using Application.Identity.Interfaces;

namespace Application.Common.Extensions
{
    /// <summary>
    /// Contains extension-methods for type <see cref="IIdentityStorage{TItem,TItemId}"/>.
    /// </summary>
    public static class IIdentityStorageExtensions
    {
        /// <summary>
        /// Throws <see cref="NotFoundException"/> if the item with the specified <paramref name="id"/> doesn't exist.
        /// </summary>
        public static async Task ThrowIfDoesNotExistAsync<TItem, TItemId>(this IIdentityStorage<TItem, TItemId> storage,
            TItemId id)
        {
            bool itemExists = await storage.Exists(id)
                .ConfigureAwait(false);

            if (!itemExists)
            {
                throw new NotFoundException();
            }
        }
    }
}