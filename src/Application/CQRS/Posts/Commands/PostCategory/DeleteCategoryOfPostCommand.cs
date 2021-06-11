using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Commands.PostCategory
{
    public class DeleteCategoryOfPostCommand : IRequest
    {
        #region Properties

        public Guid PostId { get; set; }
        public Guid CategoryId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteCategoryOfPostCommand>
        {
            #region Fields

            private readonly IXNewsDbContext _context;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<DeleteCategoryOfPostCommand>

            public async Task<Unit> Handle(DeleteCategoryOfPostCommand request, CancellationToken cancellationToken)
            {
                Post post = await _context.Post
                                .Include(p => p.Categories)
                                .SingleOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken)
                                .ConfigureAwait(false)
                            ?? throw new NotFoundException(nameof(Post), request.PostId);

                Category category = await _context.Category
                                        .FindAsync(request.CategoryId)
                                        .ConfigureAwait(false)
                                    ?? throw new NotFoundException(nameof(Category), request.CategoryId);

                post.Categories.Remove(category);
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                
                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}