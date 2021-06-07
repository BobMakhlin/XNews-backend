using System.Threading.Tasks;
using Application.CQRS.Posts.Commands;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Commands
{
    public class CreatePostCommandValidators : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidators(IXNewsDbContext context,
            IIdentityStorage<ApplicationUser, string> userStorage)
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(PostValidationOptions.ContentMinLength, PostValidationOptions.ContentMaxLength);

            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(PostValidationOptions.TitleMinLength, PostValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Post, p => p.Title);

            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}