using Application.CQRS.Roles.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Commands
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