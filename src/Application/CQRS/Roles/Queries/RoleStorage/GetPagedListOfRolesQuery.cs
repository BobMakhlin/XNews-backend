using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Roles.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Roles.Queries.RoleStorage
{
    public class GetPagedListOfRolesQuery : IRequest<IPagedList<RoleDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfRolesQuery, IPagedList<RoleDto>>
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
            
            #region IRequestHandler<GetPagedListOfRolesQuery, IPagedList<RoleDto>>

            public async Task<IPagedList<RoleDto>> Handle(GetPagedListOfRolesQuery request, CancellationToken cancellationToken)
            {
                return await _roleStorage
                    .GetAll()
                    .OrderBy(role => role.Id)
                    .ProjectTo<RoleDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}