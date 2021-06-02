using Application.CQRS.CommentRates.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.CommentRates.Commands
{
    public class DeleteCommentRateCommandValidator : AbstractValidator<DeleteCommentRateCommand>
    {
        public DeleteCommentRateCommandValidator()
        {
            RuleFor(c => c.CommentRateId)
                .NotEmpty();
        }
    }
}