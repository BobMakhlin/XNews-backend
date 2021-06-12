using Application.CQRS.Comments.Commands.CommentRate;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentRate
{
    public class AddRateToCommentCommandValidator : AbstractValidator<AddRateToCommentCommand>
    {
        public AddRateToCommentCommandValidator(IXNewsDbContext context)
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();

            RuleFor(c => c.Rate)
                .In(CommentRateValidationOptions.NegativeRate, CommentRateValidationOptions.PositiveRate);

            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => new object[] {c.UserId, c.CommentId})
                .UniqueForColumnsCombinationInDbSet(context.CommentRate,
                    commentRate => new {commentRate.UserId, commentRate.CommentId})
                .WithMessage("User has already rated this comment");
        }
    }
}