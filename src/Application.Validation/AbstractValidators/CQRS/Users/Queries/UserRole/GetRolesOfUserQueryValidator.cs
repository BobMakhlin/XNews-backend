using Application.CQRS.Users.Queries.UserRole;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserRole
{
    public class GetRolesOfUserQueryValidator : AbstractValidator<GetRolesOfUserQuery>
    {
        public GetRolesOfUserQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}