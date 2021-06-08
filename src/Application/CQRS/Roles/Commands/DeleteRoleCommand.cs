using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Roles.Commands
{
    public class DeleteRoleCommand : IRequest
    {
        #region Properties

        public string RoleId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteRoleCommand>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationRole, string> _roleStorage;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationRole, string> roleStorage)
            {
                _roleStorage = roleStorage;
            }

            #endregion
            
            #region IRequestHandler<DeleteRoleCommand>

            public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
            {
                ApplicationRole roleToDelete = await _roleStorage.FindByIdAsync(request.RoleId)
                                                   .ConfigureAwait(false)
                                               ?? throw new NotFoundException();

                IIdentityResult identityResult = await _roleStorage.DeleteAsync(roleToDelete)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}