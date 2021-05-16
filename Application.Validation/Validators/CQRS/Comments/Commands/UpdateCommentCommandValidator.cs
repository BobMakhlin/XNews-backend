using Application.CQRS.Comments.Commands;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Comments.Commands
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(CommentValidationOptions.ContentMinLength, CommentValidationOptions.ContentMaxLength);
        }
    }
}