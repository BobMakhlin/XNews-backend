using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Categories.Models;
using Application.CQRS.Comments.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Posts.Queries
{
    public class GetAllCommentsOfPostQuery : IRequest<IEnumerable<CommentDto>>
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetAllCommentsOfPostQuery, IEnumerable<CommentDto>>
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

            public async Task<IEnumerable<CommentDto>> Handle(GetAllCommentsOfPostQuery request, CancellationToken cancellationToken)
            {
                return await _context.Post
                    .Where(p => p.PostId == request.PostId)
                    .Take(1)
                    .SelectMany(p => p.Comments)
                    .ProjectToListAsync<CommentDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}