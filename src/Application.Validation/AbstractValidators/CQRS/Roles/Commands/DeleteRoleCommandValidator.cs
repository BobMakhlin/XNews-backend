using Application.CQRS.Roles.Commands;
using Application.CQRS.Roles.Commands.RoleStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Commands
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