using Application.CQRS.PostRates.Commands;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.PostRates.Commands
{
    public class CreatePostRateCommandValidator : AbstractValidator<CreatePostRateCommand>
    {
        public CreatePostRateCommandValidator()
        {
            RuleFor(c => c.Rate)
                .In(PostRateValidationOptions.NegativeRate, PostRateValidationOptions.PositiveRate);
        }
    }
}