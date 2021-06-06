using Application.CQRS.CommentRates.Commands;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.CommentRates.Commands
{
    public class CreateCommentRateCommandValidator : AbstractValidator<CreateCommentRateCommand>
    {
        public CreateCommentRateCommandValidator()
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