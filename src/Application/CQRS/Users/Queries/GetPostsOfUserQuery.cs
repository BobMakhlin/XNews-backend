using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Posts.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetPostsOfUserQuery : IRequest<IPagedList<PostDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; } 

        #endregion
        
        #region Classes

        public class Handler : IRequestHandler<GetPostsOfUserQuery, IPagedList<PostDto>>
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
            
            #region IRequestHandler<GetPostsOfUserQuery, IPagedList<PostDto>>

            public async Task<IPagedList<PostDto>> Handle(GetPostsOfUserQuery request, CancellationToken cancellationToken)
            {
                return await _context.Post
                    .Where(p => p.UserId == request.UserId)
                    .OrderBy(p => p.PostId)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}