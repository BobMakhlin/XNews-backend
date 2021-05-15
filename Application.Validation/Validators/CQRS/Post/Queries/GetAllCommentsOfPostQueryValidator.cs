using Application.CQRS.Posts.Queries;
using Application.Validation.Validators.Common;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Post.Queries
{
    public class GetAllCommentsOfPostQueryValidator : AbstractValidator<GetAllCommentsOfPostQuery>
    {
        public GetAllCommentsOfPostQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}