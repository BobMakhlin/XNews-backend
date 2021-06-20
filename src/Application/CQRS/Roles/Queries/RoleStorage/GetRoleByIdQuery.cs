using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Roles.Models;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Roles.Queries.RoleStorage
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        #region Properties

        public string RoleId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetRoleByIdQuery, RoleDto>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IIdentityStorage<ApplicationRole, string> _roleStorage;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationRole, string> roleStorage, IMapper mapper)
            {
                _roleStorage = roleStorage;
                _mapper = mapper;
            }       

            #endregion
            
            #region IRequestHandler<GetRoleByIdQuery, RoleDto>

            public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
            {
                return await _roleStorage
                           .GetAll()
                           .Where(role => role.Id == request.RoleId)
                           .ProjectToSingleOrDefaultAsync<RoleDto>(_mapper.ConfigurationProvider, cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException(nameof(ApplicationRole), request.RoleId);
            }

            #endregion
        }

        #endregion
    }
}