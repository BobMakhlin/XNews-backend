using Application.CQRS.Posts.Queries.PostComment;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostComment
{
    public class GetPagedListOfPostCommentsQueryValidator : AbstractValidator<GetPagedListOfPostCommentsQuery>
    {
        public GetPagedListOfPostCommentsQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}