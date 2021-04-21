using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Commands
{
    public class DeletePostCommand : IRequest
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<DeletePostCommand>
        {
            #region Fields

            private readonly IXNewsDbContext _context;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<DeletePostCommand>

            public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                Post post = await _context.Post
                                .Include(p => p.PostRates)
                                .Include(p => p.Comments)
                                .SingleOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken)
                                .ConfigureAwait(false)
                            ?? throw new NotFoundException();

                ThrowIfPostCannotBeRemove(post);
                
                MarkPostForRemove(post);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Marks <paramref name="post"/> and its <see cref="Post.PostRates"/> for remove.
            /// They will be removed from database when <see cref="DbContext.SaveChanges()"/> is called.
            /// </summary>
            /// <param name="post"></param>
            private void MarkPostForRemove(Post post)
            {
                foreach (var postRate in post.PostRates)
                {
                    _context.PostRate.Remove(postRate);
                }
                
                _context.Post.Remove(post);
            }

            /// <summary>
            /// If the given <paramref name="post"/> cannot be removed, throws an exception.
            /// </summary>
            /// <param name="post"></param>
            /// <exception cref="InvalidOperationException">Post has some <see cref="Post.Comments"/></exception>
            private void ThrowIfPostCannotBeRemove(Post post)
            {
                if (post.Comments.Any())
                {
                    throw new InvalidOperationException("Can't remove post that has comments");
                }
            }

            #endregion
        }
    }
}