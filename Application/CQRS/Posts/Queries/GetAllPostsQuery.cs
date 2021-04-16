using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Posts.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Posts.Queries
{
    public class GetAllPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        public class Handler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>
        {
            #region Fields

            private IXNewsDbContext _context;
            private IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetAllPostsQuery, IEnumerable<PostDto>>

            public async Task<IEnumerable<PostDto>> Handle(GetAllPostsQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Post
                    .ProjectToListAsync<PostDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}