using Application.CQRS.Posts.Commands;
using Application.CQRS.Posts.Commands.PostRate;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.PostRates.Commands
{
    public class CreatePostRateCommandValidator : AbstractValidator<AddRateToPostCommand>
    {
        public CreatePostRateCommandValidator()
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