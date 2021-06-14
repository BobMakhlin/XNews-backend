using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.PostRates.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries.UserPostRate
{
    public class GetPagedListOfPostRatesMadeByUserQuery : IRequest<IPagedList<PostRateDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfPostRatesMadeByUserQuery, IPagedList<PostRateDto>>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IXNewsDbContext _context;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper,
                IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _context = context;
                _mapper = mapper;
                _userStorage = userStorage;
            }

            #endregion

            #region IRequestHandler<GetPagedListOfPostRatesMadeByUserQuery, IPagedList<PostRateDto>>

            public async Task<IPagedList<PostRateDto>> Handle(GetPagedListOfPostRatesMadeByUserQuery request,
                CancellationToken cancellationToken)
            {
                IPagedList<PostRateDto> postRates = await _context.PostRate
                    .Where(pr => pr.UserId == request.UserId)
                    .OrderBy(pr => pr.PostRateId)
                    .ProjectTo<PostRateDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (postRates.TotalItemsCount == 0)
                {
                    await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                        .ConfigureAwait(false);
                }
                
                return postRates;
            }

            #endregion
        }

        #endregion
    }
}