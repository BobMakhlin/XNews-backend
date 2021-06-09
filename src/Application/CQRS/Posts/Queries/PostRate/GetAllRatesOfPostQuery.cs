using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.PostRates.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries.PostRate
{
    public class GetAllRatesOfPostQuery : IRequest<IEnumerable<PostRateDto>>
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetAllRatesOfPostQuery, IEnumerable<PostRateDto>>
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
            
            #region IRequestHandler<GetAllRatesOfPostQuery, IEnumerable<PostRateDto>>

            public async Task<IEnumerable<PostRateDto>> Handle(GetAllRatesOfPostQuery request,
                CancellationToken cancellationToken)
            {
                List<PostRateDto> postRates = await _context.PostRate
                    .Where(pr => pr.PostId == request.PostId)
                    .ProjectToListAsync<PostRateDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
                if (postRates.Count > 0)
                {
                    return postRates;
                }

                bool postExists = await _context.Post
                    .AnyAsync(p => p.PostId == request.PostId, cancellationToken)
                    .ConfigureAwait(false);
                return postExists ? Enumerable.Empty<PostRateDto>() : throw new NotFoundException();
            }

            #endregion
        }
    }
}