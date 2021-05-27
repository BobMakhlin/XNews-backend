using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Roles.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetRolesOfUserQuery : IRequest<IEnumerable<RoleDto>>
    {
        public string UserId { get; set; }

        public class Handler : IRequestHandler<GetRolesOfUserQuery, IEnumerable<RoleDto>>
        {
            #region Fields

            private readonly IIdentityUserService<ApplicationUser, ApplicationRole> _authorizationService;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IIdentityUserService<ApplicationUser, ApplicationRole> authorizationService,
                IMapper mapper)
            {
                _authorizationService = authorizationService;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetRolesOfUserQuery, IEnumerable<RoleDto>>

            public async Task<IEnumerable<RoleDto>> Handle(GetRolesOfUserQuery request,
                CancellationToken cancellationToken)
            {
                IEnumerable<ApplicationRole> roles = await _authorizationService.GetRolesOfUserAsync(request.UserId)
                    .ConfigureAwait(false);
                return _mapper.Map<IEnumerable<RoleDto>>(roles);
            }

            #endregion
        }
    }
}