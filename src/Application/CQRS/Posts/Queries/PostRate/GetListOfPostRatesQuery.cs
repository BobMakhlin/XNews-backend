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
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries.PostRate
{
    public class GetListOfPostRatesQuery : IRequest<IEnumerable<PostRateDto>>
    {
        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetListOfPostRatesQuery, IEnumerable<PostRateDto>>
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

            #region IRequestHandler<GetListOfPostRatesQuery, IEnumerable<PostRateDto>>

            public async Task<IEnumerable<PostRateDto>> Handle(GetListOfPostRatesQuery request,
                CancellationToken cancellationToken)
            {
                List<PostRateDto> postRates = await _context.PostRate
                    .AsNoTracking()
                    .Where(pr => pr.PostId == request.PostId)
                    .ProjectToListAsync<PostRateDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);

                if (postRates.Count == 0)
                {
                    await _context.Post.ThrowIfDoesNotExistAsync(request.PostId)
                        .ConfigureAwait(false);
                }

                return postRates;
            }

            #endregion
        }

        #endregion
    }
}