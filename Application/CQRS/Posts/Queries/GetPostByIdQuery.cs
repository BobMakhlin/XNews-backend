using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Posts.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries
{
    public class GetPostByIdQuery : IRequest<PostDto>
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetPostByIdQuery, PostDto>
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
            
            #region IRequestHandler<GetPostByIdQuery, PostDto>

            public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
            {
                Post post = await _context.Post
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);
                return _mapper.Map<PostDto>(post);
            }

            #endregion
        }
    }
}