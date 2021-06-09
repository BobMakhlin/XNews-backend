using Application.CQRS.Roles.Commands.RoleStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Commands.RoleStorage
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(c => c.RoleId)
                .NotEmpty();
        }
    }
}