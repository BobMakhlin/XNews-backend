using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Posts.Commands
{
    public class DeletePostCommand : IRequest
    {
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<DeletePostCommand>
        {
            #region Fields

            private IXNewsDbContext _context;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<DeletePostCommand>

            public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                Post post = await _context.Post.FindAsync(request.PostId)
                            ?? throw new NotFoundException();

                _context.Post.Remove(post);
                await _context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }

            #endregion
        }
    }
}