using Application.CQRS.Roles.Commands.RoleStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Commands.RoleStorage
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(c => c.RoleId)
                .NotEmpty();
        }
    }
}