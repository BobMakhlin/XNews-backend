using Application.CQRS.Users.Queries;
using Application.CQRS.Users.Queries.UserPost;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries
{
    public class GetPostsOfUserQueryValidator : AbstractValidator<GetPostsOfUserQuery>
    {
        public GetPostsOfUserQueryValidator()
        {
            Include(new PaginationRequestValidator());
            
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}