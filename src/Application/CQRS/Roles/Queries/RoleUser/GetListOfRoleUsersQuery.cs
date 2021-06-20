using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Users.Models;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Roles.Queries.RoleUser
{
    public class GetListOfRoleUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        #region Properties

        public string RoleId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetListOfRoleUsersQuery, IEnumerable<UserDto>>
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
            
            #region IRequestHandler<GetListOfRoleUsersQuery, IEnumerable<UserDto>>

            public async Task<IEnumerable<UserDto>> Handle(GetListOfRoleUsersQuery request,
                CancellationToken cancellationToken)
            {
                ApplicationRole role = await _roleStorage.FindByIdAsync(request.RoleId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException(nameof(ApplicationRole), request.RoleId);

                return await _userRoleService.GetRoleUsers(role)
                    .ProjectToListAsync<UserDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}