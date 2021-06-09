using Application.CQRS.Posts.Commands.PostRate;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostRate
{
    public class AddRateToPostCommandValidator : AbstractValidator<AddRateToPostCommand>
    {
        public AddRateToPostCommandValidator()
        {
            RuleFor(c => c.PostId)
                .NotEmpty();
            
            RuleFor(c => c.Rate)
                .In(PostRateValidationOptions.NegativeRate, PostRateValidationOptions.PositiveRate);

            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}