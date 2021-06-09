using Application.CQRS.Comments.Commands.CommentRate;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentRate
{
    public class RemoveRateOfCommentCommandValidator : AbstractValidator<RemoveRateOfCommentCommand>
    {
        public RemoveRateOfCommentCommandValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();

            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}