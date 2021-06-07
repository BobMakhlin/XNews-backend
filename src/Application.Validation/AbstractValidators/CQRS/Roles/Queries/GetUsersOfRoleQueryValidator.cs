using Application.CQRS.Roles.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Queries
{
    public class GetUsersOfRoleQueryValidator : AbstractValidator<GetUsersOfRoleQuery>
    {
        public GetUsersOfRoleQueryValidator()
        {
            RuleFor(q => q.RoleId)
                .NotEmpty();
        }
    }
}