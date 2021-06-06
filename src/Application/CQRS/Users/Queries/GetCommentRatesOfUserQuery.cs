using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetCommentRatesOfUserQuery : IRequest<IPagedList<CommentRateDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetCommentRatesOfUserQuery, IPagedList<CommentRateDto>>
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
            
            #region IRequestHandler<GetCommentRatesOfUserQuery, IPagedList<CommentRateDto>>

            public async Task<IPagedList<CommentRateDto>> Handle(GetCommentRatesOfUserQuery request, CancellationToken cancellationToken)
            {
                return await _context.CommentRate
                    .Where(cr => cr.UserId == request.UserId)
                    .OrderBy(c => c.CommentRateId)
                    .ProjectTo<CommentRateDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}