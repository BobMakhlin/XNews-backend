using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Commands.PostStorage
{
    public class DeletePostCommand : IRequest
    {
        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region Classes

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
                                .Include(p => p.Comments)
                                .SingleOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken)
                                .ConfigureAwait(false)
                            ?? throw new NotFoundException(nameof(Post), request.PostId);

                MarkPostForRemove(post);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Marks <paramref name="post"/> for remove.
            /// They will be removed from database when <see cref="DbContext.SaveChanges()"/> is called.
            /// </summary>
            /// <param name="post"></param>
            private void MarkPostForRemove(Post post)
            {
                _context.Post.Remove(post);
            }

            #endregion
        }

        #endregion
    }
}