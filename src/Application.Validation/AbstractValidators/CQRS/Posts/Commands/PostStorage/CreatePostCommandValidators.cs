using Application.CQRS.Posts.Commands.PostStorage;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostStorage
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