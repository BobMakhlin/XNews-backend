using Application.CQRS.Comments.Commands.CommentStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentStorage
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();
        }
    }
}