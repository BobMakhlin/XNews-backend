using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces;
using Application.Persistence.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Commands.PostRate
{
    public class RemoveRateOfPostCommand : IRequest
    {
        #region Properties

        public Guid PostId { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<RemoveRateOfPostCommand>
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
            
            #region IRequestHandler<RemoveRateOfPostCommand>

            public async Task<Unit> Handle(RemoveRateOfPostCommand request, CancellationToken cancellationToken)
            {
                Domain.Primary.Entities.PostRate postRate = await _context.PostRate
                    .Where(pr => pr.PostId == request.PostId && pr.UserId == request.UserId)
                    .SingleOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (postRate != null)
                {
                    _context.PostRate.Remove(postRate);
                    await _context
                        .SaveChangesAsync(cancellationToken)
                        .ConfigureAwait(false);

                    return Unit.Value;
                }
                
                await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                    .ConfigureAwait(false);
                await _context.Post.ThrowIfDoesNotExistAsync(request.PostId)
                    .ConfigureAwait(false);

                throw new NotFoundException(nameof(Domain.Primary.Entities.PostRate), new object[] { });

            }

            #endregion
        }

        #endregion
    }
}