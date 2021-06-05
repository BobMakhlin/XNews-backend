using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Roles.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Queries
{
    public class GetRolesOfUserQuery : IRequest<IEnumerable<RoleDto>>
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetRolesOfUserQuery, IEnumerable<RoleDto>>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser> _userStorage;
            private readonly IUserRoleService<ApplicationUser, ApplicationRole> _userRoleService;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IUserRoleService<ApplicationUser, ApplicationRole> userRoleService,
                IIdentityStorage<ApplicationUser> userStorage, IMapper mapper)
            {
                _userRoleService = userRoleService;
                _userStorage = userStorage;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetRolesOfUserQuery, IEnumerable<RoleDto>>

            public async Task<IEnumerable<RoleDto>> Handle(GetRolesOfUserQuery request,
                CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userStorage.GetAll()
                    .SingleOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                    .ConfigureAwait(false);
                if (user == null)
                {
                    throw new NotFoundException();
                }

                return await _userRoleService.GetUserRoles(user)
                    .ProjectToListAsync<RoleDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}