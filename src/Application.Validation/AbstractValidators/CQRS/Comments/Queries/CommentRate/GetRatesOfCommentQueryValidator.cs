using Application.CQRS.Comments.Queries.CommentRate;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Queries.CommentRate
{
    public class GetRatesOfCommentQueryValidator : AbstractValidator<GetRatesOfCommentQuery>
    {
        public GetRatesOfCommentQueryValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();
        }
    }
}