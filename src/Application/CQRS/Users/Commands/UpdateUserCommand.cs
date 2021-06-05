using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Users.Commands
{
    public class UpdateUserCommand : IRequest
    {
        #region Properties

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<UpdateUserCommand>
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

            #region IRequestHandler<UpdateUserCommand>

            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser userToUpdate = await _userStorage.FindByIdAsync(request.UserId)
                    .ConfigureAwait(false);
                if (userToUpdate == null)
                {
                    throw new NotFoundException();
                }
                
                UpdateApplicationUserProperties(userToUpdate, request);

                IIdentityResult identityResult = await _userStorage.UpdateAsync(userToUpdate)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();
                
                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Updates <paramref name="user"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="user">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="user"/> parameter</param>
            private void UpdateApplicationUserProperties(ApplicationUser user, UpdateUserCommand request)
            {
                user.UserName = request.UserName;
                user.Email = request.Email;
                user.PhoneNumber = request.PhoneNumber;
            }

            #endregion
        }

        #endregion
    }
}