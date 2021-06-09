using Application.CQRS.Roles.Queries;
using Application.CQRS.Roles.Queries.RoleUser;
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