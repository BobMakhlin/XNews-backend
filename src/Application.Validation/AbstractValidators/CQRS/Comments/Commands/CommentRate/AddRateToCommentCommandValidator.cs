using Application.CQRS.Comments.Commands.CommentRate;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentRate
{
    public class AddRateToCommentCommandValidator : AbstractValidator<AddRateToCommentCommand>
    {
        public AddRateToCommentCommandValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();
            
            RuleFor(c => c.Rate)
                .In(CommentRateValidationOptions.NegativeRate, CommentRateValidationOptions.PositiveRate);

            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}