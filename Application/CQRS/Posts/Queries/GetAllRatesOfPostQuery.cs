﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.PostRates.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Posts.Queries
{
    public class GetAllRatesOfPostQuery : IRequest<IEnumerable<PostRateDto>>
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetAllRatesOfPostQuery, IEnumerable<PostRateDto>>
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
            
            #region IRequestHandler<GetAllRatesOfPostQuery, IEnumerable<PostRateDto>>

            public async Task<IEnumerable<PostRateDto>> Handle(GetAllRatesOfPostQuery request, CancellationToken cancellationToken)
            {
                return await _context.Post
                    .Where(p => p.PostId == request.PostId)
                    .Take(1)
                    .SelectMany(p => p.PostRates)
                    .ProjectToListAsync<PostRateDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}