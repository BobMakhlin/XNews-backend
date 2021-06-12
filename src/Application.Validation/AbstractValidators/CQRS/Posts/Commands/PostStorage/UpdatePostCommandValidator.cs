using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Posts.Commands.PostStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostStorage
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        #region Fields

        private readonly IXNewsDbContext _context;

        #endregion

        #region Constructors

        public UpdatePostCommandValidator(IXNewsDbContext context)
        {
            _context = context;

            RuleFor(c => c.PostId)
                .NotEmpty();

            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(PostValidationOptions.ContentMinLength, PostValidationOptions.ContentMaxLength);

            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(PostValidationOptions.TitleMinLength, PostValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Post, p => p.Title)
                .WhenAsync(PostTitleWasUpdated);

            RuleFor(c => c.UserId)
                .NotEmpty();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the post title was changed in the current update.
        /// </summary>
        /// <param name="command">
        /// Contains information about the current update of the post.
        /// </param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> PostTitleWasUpdated(UpdatePostCommand command, CancellationToken token)
        {
            string currentPostTitle = await _context.Post
                .Where(p => p.PostId == command.PostId)
                .Select(p => p.Title)
                .SingleOrDefaultAsync(token);
            return currentPostTitle != command.Title;
        }

        #endregion
    }
}