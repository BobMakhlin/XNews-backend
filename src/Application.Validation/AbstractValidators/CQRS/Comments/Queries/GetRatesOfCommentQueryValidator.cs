using Application.CQRS.Comments.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Queries
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