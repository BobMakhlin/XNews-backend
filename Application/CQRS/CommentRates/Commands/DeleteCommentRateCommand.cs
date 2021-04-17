using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.CommentRates.Commands
{
    public class DeleteCommentRateCommand : IRequest
    {
        public Guid CommentRateId { get; set; }

        public class Handler : IRequestHandler<DeleteCommentRateCommand>
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

            #region IRequestHandler<DeleteCommentRateCommand>

            public async Task<Unit> Handle(DeleteCommentRateCommand request, CancellationToken cancellationToken)
            {
                CommentRate commentRate = await _context.CommentRate
                    .FindAsync(request.CommentRateId)
                    .ConfigureAwait(false);
                if (commentRate == null)
                {
                    throw new NotFoundException();
                }

                _context.CommentRate.Remove(commentRate);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
        }
    }
}