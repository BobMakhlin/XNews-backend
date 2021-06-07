using Application.CQRS.Users.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries
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