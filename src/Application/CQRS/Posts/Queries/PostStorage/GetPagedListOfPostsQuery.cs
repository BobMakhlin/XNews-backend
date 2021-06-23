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
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries.PostStorage
{
    public class GetPagedListOfPostsQuery : IRequest<IPagedList<PostDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfPostsQuery, IPagedList<PostDto>>
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

            #region IRequestHandler<GetPagedListOfPostsQuery, IEnumerable<PostDto>>

            public async Task<IPagedList<PostDto>> Handle(GetPagedListOfPostsQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Post
                    .AsNoTracking()
                    .OrderBy(p => p.PostId)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}