using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Roles.Models;
using Application.Identity.Entities;
using Application.Identity.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Users.Queries.UserRole
{
    public class GetListOfUserRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetListOfUserRolesQuery, IEnumerable<RoleDto>>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;
            private readonly IUserRoleService<ApplicationUser, ApplicationRole> _userRoleService;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IUserRoleService<ApplicationUser, ApplicationRole> userRoleService,
                IIdentityStorage<ApplicationUser, string> userStorage, IMapper mapper)
            {
                _userRoleService = userRoleService;
                _userStorage = userStorage;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetListOfUserRolesQuery, IEnumerable<RoleDto>>

            public async Task<IEnumerable<RoleDto>> Handle(GetListOfUserRolesQuery request,
                CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userStorage.FindByIdAsync(request.UserId)
                                           .ConfigureAwait(false)
                                       ?? throw new NotFoundException(nameof(ApplicationUser), request.UserId);

                return await _userRoleService.GetUserRoles(user)
                    .ProjectToListAsync<RoleDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}