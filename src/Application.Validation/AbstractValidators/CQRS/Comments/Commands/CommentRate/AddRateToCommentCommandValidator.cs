using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Comments.Commands.CommentRate;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentRate
{
    public class AddRateToCommentCommandValidator : AbstractValidator<AddRateToCommentCommand>
    {
        #region Fields

        private readonly IXNewsDbContext _context;

        #endregion

        #region Constructors

        public AddRateToCommentCommandValidator(IXNewsDbContext context)
        {
            _context = context;

            RuleFor(c => c.CommentId)
                .NotEmpty();

            RuleFor(c => c.Rate)
                .In(CommentRateValidationOptions.NegativeRate, CommentRateValidationOptions.PositiveRate);

            RuleFor(c => c.UserId)
                .NotEmpty()
                .MustAsync(NotHaveRatedTheCurrentComment).WithMessage("User has already rated this comment");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the user with the specified <paramref name="userId"/> has already rated the comment
        /// with the specified <paramref name="command.CommentId"/>.
        /// </summary>
        private async Task<bool> NotHaveRatedTheCurrentComment(AddRateToCommentCommand command, string userId,
            CancellationToken token)
        {
            return !await _context.CommentRate.AnyAsync(commentRate =>
                    commentRate.UserId == userId && commentRate.CommentId == command.CommentId, token)
                .ConfigureAwait(false);
        }

        #endregion
    }
}