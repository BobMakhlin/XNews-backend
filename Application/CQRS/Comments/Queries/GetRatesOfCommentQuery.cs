using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.CommentRates.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Comments.Queries
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

            public async Task<IEnumerable<CommentRateDto>> Handle(GetRatesOfCommentQuery request, CancellationToken cancellationToken)
            {
                return await _context.Comment
                    .Where(c => c.CommentId == request.CommentId)
                    .Take(1)
                    .SelectMany(c => c.CommentRates)
                    .ProjectToListAsync<CommentRateDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}