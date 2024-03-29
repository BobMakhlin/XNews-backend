﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Categories.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries.PostCategory
{
    public class GetListOfPostCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetListOfPostCategoriesQuery, IEnumerable<CategoryDto>>
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

            #region IRequestHandler<GetListOfPostCategoriesQuery, IEnumerable<CategoryDto>>

            public async Task<IEnumerable<CategoryDto>> Handle(GetListOfPostCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                List<CategoryDto> categories = await _context.Category
                    .AsNoTracking()
                    .Where(c => c.Posts.Any(p => p.PostId == request.PostId))
                    .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);

                if (categories.Count == 0)
                {
                    await _context.Post.ThrowIfDoesNotExistAsync(request.PostId)
                        .ConfigureAwait(false);
                }

                return categories;
            }

            #endregion
        }

        #endregion
    }
}