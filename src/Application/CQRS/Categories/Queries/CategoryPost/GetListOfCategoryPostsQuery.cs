using Application.Common.Extensions;
using Application.CQRS.Posts.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CQRS.Categories.Queries.CategoryPost
{
    public class GetListOfCategoryPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        #region Properties

        public Guid CategoryId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetListOfCategoryPostsQuery, IEnumerable<PostDto>>
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

            #region IRequestHandler<GetListOfCategoryPostsQuery, IEnumerable<PostDto>>

            public async Task<IEnumerable<PostDto>> Handle(GetListOfCategoryPostsQuery request,
                CancellationToken cancellationToken)
            {
                List<PostDto> posts = await _context.Post
                    .AsNoTracking()
                    .Where(p => p.Categories.Any(c => c.CategoryId == request.CategoryId))
                    .ProjectToListAsync<PostDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);

                if (posts.Count == 0)
                {
                    await _context.Category.ThrowIfDoesNotExistAsync(request.CategoryId)
                        .ConfigureAwait(false);
                }

                return posts;
            }

            #endregion
        }

        #endregion
    }
}
