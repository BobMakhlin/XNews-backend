using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Users.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Roles.Queries
{
    public class GetUsersOfRoleQuery : IRequest<IEnumerable<UserDto>>
    {
        #region Properties

        public string RoleId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetUsersOfRoleQuery, IEnumerable<UserDto>>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IIdentityStorage<ApplicationRole, string> _roleStorage;
            private readonly IUserRoleService<ApplicationUser, ApplicationRole> _userRoleService;

            #endregion

            #region Constructors

            public Handler(IUserRoleService<ApplicationUser, ApplicationRole> userRoleService,
                IIdentityStorage<ApplicationRole, string> roleStorage, IMapper mapper)
            {
                _userRoleService = userRoleService;
                _roleStorage = roleStorage;
                _mapper = mapper;
            }

            #endregion
            
            #region IRequestHandler<GetUsersOfRoleQuery, IEnumerable<UserDto>>

            public async Task<IEnumerable<UserDto>> Handle(GetUsersOfRoleQuery request,
                CancellationToken cancellationToken)
            {
                ApplicationRole role = await _roleStorage.FindByIdAsync(request.RoleId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException();

                return await _userRoleService.GetRoleUsers(role)
                    .ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}