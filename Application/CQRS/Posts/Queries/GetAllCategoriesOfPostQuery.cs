using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Categories.Models;
using Application.CQRS.Posts.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries
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
                return await _context.Post
                    .Where(p => p.PostId == request.PostId)
                    .Take(1)
                    .SelectMany(p => p.Categories)
                    .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}

