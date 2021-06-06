using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.CommentRates.Commands
{
    public class CreateCommentRateCommand : IRequest<Guid>
    {
        public Guid CommentId { get; set; }
        public double Rate { get; set; }
        public string UserId { get; set; }

        public class Handler : IRequestHandler<CreateCommentRateCommand, Guid>
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
            
            #region IRequestHandler<AddCommentRateCommand, Guid>

            public async Task<Guid> Handle(CreateCommentRateCommand request, CancellationToken cancellationToken)
            {
                CommentRate commentRate = ConvertToCommentRate(request);

                await CreateCommentRateAsync(commentRate, cancellationToken)
                    .ConfigureAwait(false);

                return commentRate.CommentRateId;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="CommentRate"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="CommentRate"/></returns>
            private CommentRate ConvertToCommentRate(CreateCommentRateCommand command)
            {
                return new()
                {
                    CommentId = command.CommentId,
                    Rate = command.Rate,
                    UserId = command.UserId
                };
            }

            private async Task CreateCommentRateAsync(CommentRate commentRate, CancellationToken cancellationToken)
            {
                await _context.CommentRate.AddAsync(commentRate, cancellationToken)
                    .ConfigureAwait(false);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
            
            #endregion
        }
    }
}