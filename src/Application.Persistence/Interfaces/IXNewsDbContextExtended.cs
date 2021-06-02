using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Primary.Entities;

namespace Application.Persistence.Interfaces
{
    /// <summary>
    /// An interface, that extends type <see cref="IXNewsDbContext"/>
    /// and provides calls to database functions, procedures, etc, encapsulated inside of methods.
    /// </summary>
    public interface IXNewsDbContextExtended : IXNewsDbContext
    {
        /// <summary>
        /// Returns comments of a post with the specified <paramref name="postId"/>.
        /// Paginates them by the specified <paramref name="pageNumber"/> and <paramref name="pageSize"/>.
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// Flat list of comments - property <see cref="Comment.InverseParentComment"/> of each comment is empty.
        /// </returns>
        IQueryable<Comment> GetPostComments(Guid postId, int pageNumber, int pageSize);
    }
}