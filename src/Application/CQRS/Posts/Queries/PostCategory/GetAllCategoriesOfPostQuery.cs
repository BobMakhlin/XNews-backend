using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Categories.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries.PostCategory
{
    public class GetAllCategoriesOfPostQuery : IRequest<IEnumerable<CategoryDto>>
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetAllCategoriesOfPostQuery, IEnumerable<CategoryDto>>
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

            #region IRequestHandler<GetAllCategoriesOfPostQuery, IEnumerable<CategoryDto>>

            public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesOfPostQuery request,
                CancellationToken cancellationToken)
            {
                List<CategoryDto> categories = await _context.Category
                    .Where(c => c.Posts.Any(p => p.PostId == request.PostId))
                    .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
                if (categories.Count > 0)
                {
                    return categories;
                }

                bool postExists = await _context.Post
                    .AnyAsync(p => p.PostId == request.PostId, cancellationToken)
                    .ConfigureAwait(false);
                return postExists ? Enumerable.Empty<CategoryDto>() : throw new NotFoundException();
            }

            #endregion
        }
    }
}

