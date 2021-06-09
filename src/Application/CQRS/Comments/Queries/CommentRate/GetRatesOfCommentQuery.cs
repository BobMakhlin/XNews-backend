using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.CommentRates.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Comments.Queries.CommentRate
{
    public class GetRatesOfCommentQuery : IRequest<IEnumerable<CommentRateDto>>
    {
        public Guid CommentId { get; set; }

        public class Handler : IRequestHandler<GetRatesOfCommentQuery, IEnumerable<CommentRateDto>>
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

            #region IRequestHandler<GetRatesOfCommentQuery, IEnumerable<CommentRateDto>>

            public async Task<IEnumerable<CommentRateDto>> Handle(GetRatesOfCommentQuery request,
                CancellationToken cancellationToken)
            {
                List<CommentRateDto> rates = await _context.CommentRate
                    .Where(cr => cr.CommentId == request.CommentId)
                    .ProjectToListAsync<CommentRateDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
                if (rates.Count > 0)
                {
                    return rates;
                }

                bool commentExists = await _context.Comment
                    .AnyAsync(c => c.CommentId == request.CommentId, cancellationToken)
                    .ConfigureAwait(false);
                return commentExists ? Enumerable.Empty<CommentRateDto>() : throw new NotFoundException();
            }

            #endregion
        }
    }
}