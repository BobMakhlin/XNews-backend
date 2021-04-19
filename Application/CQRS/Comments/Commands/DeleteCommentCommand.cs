using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Comments.Commands
{
    public class DeleteCommentCommand : IRequest
    {
        public Guid CommentId { get; set; }

        public class Handler : IRequestHandler<DeleteCommentCommand>
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

            #region IRequestHandler<DeleteCommentCommand>

            public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                Comment comment = await _context.Comment
                                      .Include(c => c.CommentRates)
                                      .SingleOrDefaultAsync(c => c.CommentId == request.CommentId, cancellationToken)
                                      .ConfigureAwait(false)
                                  ?? throw new NotFoundException();

                MarkCommentForRemove(comment);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
                
                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Marks <paramref name="comment"/> and its <see cref="Comment.CommentRates"/> for remove.
            /// They will be removed from database when <see cref="DbContext.SaveChanges()"/> is called.
            /// </summary>
            /// <param name="comment"></param>
            private void MarkCommentForRemove(Comment comment)
            {
                foreach (var commentRate in comment.CommentRates)
                {
                    _context.CommentRate.Remove(commentRate);
                }

                _context.Comment.Remove(comment);
            }

            #endregion
        }
    }
}