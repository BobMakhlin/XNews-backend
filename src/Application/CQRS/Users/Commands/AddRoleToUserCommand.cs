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

            private readonly IIdentityStorage<ApplicationUser> _userStorage;
            private readonly IIdentityStorage<ApplicationRole> _roleStorage;
            private readonly IUserRoleService<ApplicationUser, ApplicationRole> _userRoleService;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser> userStorage, IIdentityStorage<ApplicationRole> roleStorage,
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
                ApplicationUser user = await _userStorage.GetAll()
                    .SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                    .ConfigureAwait(false);
                if (user == null)
                {
                    throw new NotFoundException();
                }

                ApplicationRole role = await _roleStorage.GetAll()
                    .SingleOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken)
                    .ConfigureAwait(false);
                if (role == null)
                {
                    throw new NotFoundException();
                }

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