using Application.CQRS.Posts.Commands;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Post.Commands
{
    public class CreatePostCommandValidators : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidators()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(PostValidationOptions.ContentMinLength, PostValidationOptions.ContentMaxLength);

            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(PostValidationOptions.TitleMinLength, PostValidationOptions.TitleMaxLength);
        }
    }
}