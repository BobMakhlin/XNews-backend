using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Users.Commands.UserStorage
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

            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _userStorage = userStorage;
            }

            #endregion
            
            #region IRequestHandler<DeleteUserCommand>

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser userToDelete = await _userStorage.FindByIdAsync(request.UserId)
                                                   .ConfigureAwait(false)
                                               ?? throw new NotFoundException(nameof(ApplicationUser), request.UserId);

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