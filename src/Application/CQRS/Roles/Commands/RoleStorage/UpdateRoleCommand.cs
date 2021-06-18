using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Roles.Commands.RoleStorage
{
    public class UpdateRoleCommand : IRequest
    {
        #region Properties

        public string RoleId { get; set; }
        public string Name { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<UpdateRoleCommand>
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
            
            #region IRequestHandler<UpdateRoleCommand>

            public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
            {
                ApplicationRole roleToUpdate = await _roleStorage.FindByIdAsync(request.RoleId)
                                                   .ConfigureAwait(false)
                                               ?? throw new NotFoundException(nameof(ApplicationRole), request.RoleId);

                UpdateApplicationRoleProperties(roleToUpdate, request);

                IIdentityResult identityResult = await _roleStorage.UpdateAsync(roleToUpdate)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();
                
                return Unit.Value;
            }

            #endregion
            
            #region Methods

            /// <summary>
            /// Updates <paramref name="role"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="role">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="role"/> parameter</param>
            private void UpdateApplicationRoleProperties(ApplicationRole role, UpdateRoleCommand request)
            {
                role.Name = request.Name;
            }

            #endregion
        }

        #endregion
    }
}