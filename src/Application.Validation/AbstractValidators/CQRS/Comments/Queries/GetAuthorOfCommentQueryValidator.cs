using Application.CQRS.Comments.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Queries
{
    public class GetAuthorOfCommentQueryValidator : AbstractValidator<GetAuthorOfCommentQuery>
    {
        public GetAuthorOfCommentQueryValidator()
        {
            RuleFor(q => q.CommentId)
                .NotEmpty();
        }
    }
}