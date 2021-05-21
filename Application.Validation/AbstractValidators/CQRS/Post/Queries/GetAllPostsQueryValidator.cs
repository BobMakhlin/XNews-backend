using Application.CQRS.Posts.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Queries
{
    public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
    {
        public GetAllPostsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}