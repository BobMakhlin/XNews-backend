using Application.CQRS.CommentRates.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.CommentRates.Queries
{
    public class GetAuthorOfCommentRateQueryValidator : AbstractValidator<GetAuthorOfCommentRateQuery>
    {
        public GetAuthorOfCommentRateQueryValidator()
        {
            RuleFor(q => q.CommentRateId)
                .NotEmpty();
        }
    }
}