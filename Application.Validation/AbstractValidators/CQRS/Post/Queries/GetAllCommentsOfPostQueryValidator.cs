using Application.CQRS.Posts.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Queries
{
    public class GetAllCommentsOfPostQueryValidator : AbstractValidator<GetAllCommentsOfPostQuery>
    {
        public GetAllCommentsOfPostQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}