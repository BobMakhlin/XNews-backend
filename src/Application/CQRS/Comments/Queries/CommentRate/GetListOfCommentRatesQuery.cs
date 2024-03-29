﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.CommentRates.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Comments.Queries.CommentRate
{
    public class GetListOfCommentRatesQuery : IRequest<IEnumerable<CommentRateDto>>
    {
        #region Properties

        public Guid CommentId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetListOfCommentRatesQuery, IEnumerable<CommentRateDto>>
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

            #region IRequestHandler<GetListOfCommentRatesQuery, IEnumerable<CommentRateDto>>

            public async Task<IEnumerable<CommentRateDto>> Handle(GetListOfCommentRatesQuery request,
                CancellationToken cancellationToken)
            {
                List<CommentRateDto> rates = await _context.CommentRate
                    .AsNoTracking()
                    .Where(cr => cr.CommentId == request.CommentId)
                    .ProjectToListAsync<CommentRateDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);

                if (rates.Count == 0)
                {
                    await _context.Comment.ThrowIfDoesNotExistAsync(request.CommentId)
                        .ConfigureAwait(false);
                }

                return rates;
            }

            #endregion
        }

        #endregion
    }
}