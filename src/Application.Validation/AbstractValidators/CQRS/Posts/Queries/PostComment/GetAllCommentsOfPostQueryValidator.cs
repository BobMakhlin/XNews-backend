using Application.CQRS.Posts.Queries.PostComment;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostComment
{
    public class GetAllCommentsOfPostQueryValidator : AbstractValidator<GetAllCommentsOfPostQuery>
    {
        public GetAllCommentsOfPostQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}