using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Posts.Commands;
using Application.Persistence.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Post.Commands
{
    public class DeletePostCommandHandler : AbstractValidator<DeletePostCommand>
    {
        #region Fields

        private readonly IXNewsDbContext _context;

        #endregion
        
        #region Constructors

        public DeletePostCommandHandler(IXNewsDbContext context)
        {
            _context = context;

            RuleFor(c => c.PostId)
                .MustAsync(HaveNoCommentsAsync)
                .WithMessage("Cannot remove post, that has comments");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the post with the specified <paramref name="postId"/> has no comments.
        /// </summary>
        private async Task<bool> HaveNoCommentsAsync(Guid postId, CancellationToken cancellationToken)
        {
            return !await _context.Comment
                .Where(c => c.PostId == postId)
                .AnyAsync(cancellationToken);
        }

        #endregion
    }
}