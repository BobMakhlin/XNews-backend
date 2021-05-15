using Application.CQRS.Posts.Queries;
using Application.Validation.Validators.Common;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Post.Queries
{
    public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
    {
        public GetAllPostsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}