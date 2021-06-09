using Application.CQRS.Posts.Queries.PostStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostStorage
{
    public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
    {
        public GetAllPostsQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}