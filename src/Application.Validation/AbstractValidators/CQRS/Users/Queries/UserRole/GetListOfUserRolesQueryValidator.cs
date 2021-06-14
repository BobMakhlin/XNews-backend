using Application.CQRS.Users.Queries.UserRole;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserRole
{
    public class GetListOfUserRolesQueryValidator : AbstractValidator<GetListOfUserRolesQuery>
    {
        public GetListOfUserRolesQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}