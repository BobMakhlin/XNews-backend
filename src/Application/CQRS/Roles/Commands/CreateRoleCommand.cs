using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Roles.Commands
{
    public class CreateRoleCommand : IRequest<string>
    {
        #region Properties

        public string Name { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<CreateRoleCommand, string>
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
            
            #region IRequestHandler<CreateRoleCommand, string>

            public async Task<string> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {
                ApplicationRole role = ConvertToApplicationRole(request);

                IIdentityResult identityResult = await _roleStorage
                    .CreateAsync(role)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return role.Id;
            }

            #endregion
            
            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="ApplicationRole"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <returns>The created object of type <see cref="ApplicationRole"/></returns>
            private ApplicationRole ConvertToApplicationRole(CreateRoleCommand command)
            {
                return new()
                {
                    Name = command.Name
                };
            }

            #endregion
        }

        #endregion
    }
}