using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.PostRates.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetPostRatesOfUserQuery : IRequest<IPagedList<PostRateDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPostRatesOfUserQuery, IPagedList<PostRateDto>>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IXNewsDbContext _context;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetPostRatesOfUserQuery, IPagedList<PostRateDto>>

            public async Task<IPagedList<PostRateDto>> Handle(GetPostRatesOfUserQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.PostRate
                    .Where(pr => pr.UserId == request.UserId)
                    .OrderBy(pr => pr.PostRateId)
                    .ProjectTo<PostRateDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}