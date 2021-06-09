using Application.CQRS.Comments.Commands;
using Application.CQRS.Comments.Commands.CommentRate;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.CommentRates.Commands
{
    public class CreateCommentRateCommandValidator : AbstractValidator<AddRateToCommentCommand>
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