using Application.CQRS.Comments.Commands.CommentStorage;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentStorage
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(CommentValidationOptions.ContentMinLength, CommentValidationOptions.ContentMaxLength);

            RuleFor(c => c.PostId)
                .NotEmpty();

            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}