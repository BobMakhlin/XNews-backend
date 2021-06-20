using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Users.Commands.UserRole
{
    public class AddRoleToUserCommand : IRequest
    {
        #region Properties

        public string UserId { get; set; }
        public string RoleId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<AddRoleToUserCommand>
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

            #region IRequestHandler<AddRoleToUserCommand>

            public async Task<Unit> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userStorage.FindByIdAsync(request.UserId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException(nameof(ApplicationUser), request.UserId);

                ApplicationRole role = await _roleStorage.FindByIdAsync(request.RoleId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException(nameof(ApplicationRole), request.RoleId);

                IIdentityResult identityResult = await _userRoleService.AddUserToRoleAsync(user, role)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}