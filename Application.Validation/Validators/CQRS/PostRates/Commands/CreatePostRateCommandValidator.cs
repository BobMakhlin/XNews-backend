using Application.CQRS.PostRates.Commands;
using Application.Validation.Extensions;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.PostRates.Commands
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