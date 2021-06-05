using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion
        
        #region Classes

        public class Handler : IRequestHandler<DeleteUserCommand>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser> _userStorage;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser> userStorage)
            {
                _userStorage = userStorage;
            }

            #endregion
            
            #region IRequestHandler<DeleteUserCommand>

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser userToDelete = await _userStorage.GetAll()
                    .SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                    .ConfigureAwait(false);
                if (userToDelete == null)
                {
                    throw new NotFoundException();
                }

                IIdentityResult identityResult = await _userStorage.DeleteAsync(userToDelete)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}