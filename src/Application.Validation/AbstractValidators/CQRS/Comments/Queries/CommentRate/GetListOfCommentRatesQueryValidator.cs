using Application.CQRS.Comments.Queries.CommentRate;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Queries.CommentRate
{
    public class GetListOfCommentRatesQueryValidator : AbstractValidator<GetListOfCommentRatesQuery>
    {
        public GetListOfCommentRatesQueryValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();
        }
    }
}