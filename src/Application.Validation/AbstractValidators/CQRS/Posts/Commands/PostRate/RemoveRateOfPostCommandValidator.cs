using Application.CQRS.Posts.Commands.PostRate;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostRate
{
    public class RemoveRateOfPostCommandValidator : AbstractValidator<RemoveRateOfPostCommand>
    {
        public RemoveRateOfPostCommandValidator()
        {
            RuleFor(c => c.PostId)
                .NotEmpty();
            
            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}