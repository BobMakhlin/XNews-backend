using Application.CQRS.Posts.Commands;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Commands
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
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