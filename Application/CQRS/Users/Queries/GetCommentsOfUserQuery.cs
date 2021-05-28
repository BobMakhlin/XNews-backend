using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Comments.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetCommentsOfUserQuery : IRequest<IPagedList<CommentDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; } 

        #endregion

        public class Handler : IRequestHandler<GetCommentsOfUserQuery, IPagedList<CommentDto>>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetCommentsOfUserQuery, IPagedList<CommentDto>>

            public async Task<IPagedList<CommentDto>> Handle(GetCommentsOfUserQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Comment
                    .Where(c => c.UserId == request.UserId)
                    .OrderBy(p => p.PostId)
                    .ProjectTo<CommentDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}