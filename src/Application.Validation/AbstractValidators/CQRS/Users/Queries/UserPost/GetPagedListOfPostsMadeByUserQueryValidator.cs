using Application.CQRS.Users.Queries.UserPost;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserPost
{
    public class GetPagedListOfPostsMadeByUserQueryValidator : AbstractValidator<GetPagedListOfPostsMadeByUserQuery>
    {
        public GetPagedListOfPostsMadeByUserQueryValidator()
        {
            Include(new PaginationRequestValidator());
            
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}