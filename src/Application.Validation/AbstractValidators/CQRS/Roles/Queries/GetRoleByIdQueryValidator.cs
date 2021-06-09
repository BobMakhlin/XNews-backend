using Application.CQRS.Roles.Queries;
using Application.CQRS.Roles.Queries.RoleStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Queries
{
    public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidator()
        {
            RuleFor(q => q.RoleId)
                .NotEmpty();
        }
    }
}