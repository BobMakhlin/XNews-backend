using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
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

                await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                    .ConfigureAwait(false);
                await _context.Comment.ThrowIfDoesNotExistAsync(request.CommentId)
                    .ConfigureAwait(false);

                throw new NotFoundException(nameof(Domain.Primary.Entities.CommentRate), new object[] { });
            }

            #endregion
        }

        #endregion
    }
}