using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Users.Commands.UserPassword
{
    public class ChangeUserPasswordCommand : IRequest
    {
        #region Properties

        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<ChangeUserPasswordCommand>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;
            private readonly IUserPasswordService<ApplicationUser, string> _userPasswordService;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser, string> userStorage,
                IUserPasswordService<ApplicationUser, string> userPasswordService)
            {
                _userStorage = userStorage;
                _userPasswordService = userPasswordService;
            }

            #endregion

            #region IRequestHandler<ChangeUserPasswordCommand>

            public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userStorage.FindByIdAsync(request.UserId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException();

                IIdentityResult identityResult = await _userPasswordService
                    .ChangeUserPasswordAsync(user, request.CurrentPassword, request.NewPassword)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}