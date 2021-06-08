using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Users.Commands.UserRole
{
    public class DeleteRoleOfUserCommand : IRequest
    {
        #region Properties

        public string UserId { get; set; }
        public string RoleId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteRoleOfUserCommand>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;
            private readonly IIdentityStorage<ApplicationRole, string> _roleStorage;
            private readonly IUserRoleService<ApplicationUser, ApplicationRole> _userRoleService;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser, string> userStorage,
                IIdentityStorage<ApplicationRole, string> roleStorage,
                IUserRoleService<ApplicationUser, ApplicationRole> userRoleService)
            {
                _userStorage = userStorage;
                _roleStorage = roleStorage;
                _userRoleService = userRoleService;
            }

            #endregion

            #region IRequestHandler<DeleteRoleOfUserCommand>

            public async Task<Unit> Handle(DeleteRoleOfUserCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userStorage.FindByIdAsync(request.UserId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException();

                ApplicationRole role = await _roleStorage.FindByIdAsync(request.RoleId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException();

                IIdentityResult identityResult = await _userRoleService.RemoveUserFromRoleAsync(user, role)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}