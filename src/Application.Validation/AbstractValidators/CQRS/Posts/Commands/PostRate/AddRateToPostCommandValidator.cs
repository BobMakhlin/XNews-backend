using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Posts.Commands.PostRate;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostRate
{
    public class AddRateToPostCommandValidator : AbstractValidator<AddRateToPostCommand>
    {
        #region Fields

        private readonly IXNewsDbContext _context;

        #endregion

        #region Constructors

        public AddRateToPostCommandValidator(IXNewsDbContext context)
        {
            _context = context;

            RuleFor(c => c.PostId)
                .NotEmpty();

            RuleFor(c => c.Rate)
                .In(PostRateValidationOptions.NegativeRate, PostRateValidationOptions.PositiveRate);

            RuleFor(c => c.UserId)
                .NotEmpty()
                .MustAsync(NotHaveRatedTheCurrentPost).WithMessage("User has already rated this post");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the user with the specified <paramref name="userId"/> has already rated the post
        /// with the specified <paramref name="command.PostId"/>.
        /// </summary>
        private async Task<bool> NotHaveRatedTheCurrentPost(AddRateToPostCommand command, string userId,
            CancellationToken token)
        {
            return !await _context.PostRate.AnyAsync(postRate =>
                    postRate.UserId == userId && postRate.PostId == command.PostId, token)
                .ConfigureAwait(false);
        }

        #endregion
    }
}