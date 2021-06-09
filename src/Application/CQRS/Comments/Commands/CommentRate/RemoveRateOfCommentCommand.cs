using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Identity.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Comments.Commands.CommentRate
{
    public class RemoveRateOfCommentCommand : IRequest
    {
        #region Properties

        public Guid CommentId { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<RemoveRateOfCommentCommand>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _context = context;
                _userStorage = userStorage;
            }

            #endregion

            #region IRequestHandler<RemoveRateOfCommentCommand>

            public async Task<Unit> Handle(RemoveRateOfCommentCommand request, CancellationToken cancellationToken)
            {
                Domain.Primary.Entities.CommentRate commentRate = await _context.CommentRate
                    .Where(cr => cr.CommentId == request.CommentId && cr.UserId == request.UserId)
                    .SingleOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (commentRate != null)
                {
                    _context.CommentRate.Remove(commentRate);
                    await _context
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);

                    return Unit.Value;
                }

                await ThrowIfUserDoesNotExist(request.UserId)
                    .ConfigureAwait(false);
                await ThrowIfCommentDoesNotExist(request.CommentId, cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Throws an exception of type <see cref="NotFoundException"/> if the user with the specified
            /// <paramref name="userId"/> doesn't exist.
            /// </summary>
            private async Task ThrowIfUserDoesNotExist(string userId)
            {
                bool userExists = await _userStorage.Exists(userId)
                    .ConfigureAwait(false);

                if (!userExists)
                {
                    throw new NotFoundException();
                }
            }

            /// <summary>
            /// Throws an exception of type <see cref="NotFoundException"/> if the comment with the specified
            /// <paramref name="commentId"/> doesn't exist.
            /// </summary>
            private async Task ThrowIfCommentDoesNotExist(Guid commentId, CancellationToken cancellationToken)
            {
                bool commentExists = await _context.Comment.AnyAsync(c => c.CommentId == commentId,
                        cancellationToken)
                    .ConfigureAwait(false);

                if (!commentExists)
                {
                    throw new NotFoundException();
                }
            }

            #endregion
        }

        #endregion
    }
}

//
// return Unit.Value;