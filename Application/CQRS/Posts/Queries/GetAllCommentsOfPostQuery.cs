using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Comments.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Pagination.Options;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Posts.Queries
{
    public class GetAllCommentsOfPostQuery : IRequest<IPagedList<CommentDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; } = PaginationOptions.DefaultPageNumber;
        public int PageSize { get; set; } = PaginationOptions.DefaultPageSize;

        #endregion

        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetAllCommentsOfPostQuery, IPagedList<CommentDto>>
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
            
            #region IRequestHandler<GetAllCommentsOfPostQuery, IEnumerable<CommentDto>>

            public async Task<IPagedList<CommentDto>> Handle(GetAllCommentsOfPostQuery request, CancellationToken cancellationToken)
            {
                return await _context.Post
                    .Where(p => p.PostId == request.PostId)
                    .Take(1)
                    .SelectMany(p => p.Comments)
                    .OrderBy(c => c.CommentId)
                    .ProjectTo<CommentDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}