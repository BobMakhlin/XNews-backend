using Application.CQRS.Roles.Queries.RoleUser;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Queries.RoleUser
{
    public class GetListOfRoleUsersQueryValidator : AbstractValidator<GetListOfRoleUsersQuery>
    {
        public GetListOfRoleUsersQueryValidator()
        {
            RuleFor(q => q.RoleId)
                .NotEmpty();
        }
    }
}