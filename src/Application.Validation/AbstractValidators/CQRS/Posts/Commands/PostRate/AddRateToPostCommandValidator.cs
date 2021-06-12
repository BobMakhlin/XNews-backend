using Application.CQRS.Posts.Commands.PostRate;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostRate
{
    public class AddRateToPostCommandValidator : AbstractValidator<AddRateToPostCommand>
    {
        public AddRateToPostCommandValidator(IXNewsDbContext context)
        {
            RuleFor(c => c.PostId)
                .NotEmpty();

            RuleFor(c => c.Rate)
                .In(PostRateValidationOptions.NegativeRate, PostRateValidationOptions.PositiveRate);

            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => new object[] {c.UserId, c.PostId})
                .UniqueForColumnsCombinationInDbSet(context.PostRate,
                    postRate => new {postRate.UserId, postRate.PostId})
                .WithMessage("User has already rated this post");
        }
    }
}