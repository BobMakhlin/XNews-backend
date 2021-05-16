using Application.CQRS.CommentRates.Commands;
using Application.Validation.Extensions;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.CommentRates.Commands
{
    public class CreateCommentRateCommandValidator : AbstractValidator<CreateCommentRateCommand>
    {
        public CreateCommentRateCommandValidator()
        {
            RuleFor(c => c.Rate)
                .In(CommentRateValidationOptions.NegativeRate, CommentRateValidationOptions.PositiveRate);
        }
    }
}