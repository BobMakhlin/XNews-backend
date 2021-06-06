using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Users.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IPagedList<UserDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion
        
        #region Classes

        public class Handler : IRequestHandler<GetAllUsersQuery, IPagedList<UserDto>>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser, string> userStorage, IMapper mapper)
            {
                _userStorage = userStorage;
                _mapper = mapper;
            }

            #endregion
            
            #region IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>

            public async Task<IPagedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                return await _userStorage
                    .GetAll()
                    .OrderBy(user => user.Id)
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}

