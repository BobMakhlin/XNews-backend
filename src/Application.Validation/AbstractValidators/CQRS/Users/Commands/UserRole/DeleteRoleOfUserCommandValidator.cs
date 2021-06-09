using Application.CQRS.Users.Commands.UserRole;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands.UserRole
{
    public class DeleteRoleOfUserCommandValidator : AbstractValidator<DeleteRoleOfUserCommand>
    {
        public DeleteRoleOfUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();
            
            RuleFor(c => c.RoleId)
                .NotEmpty();
        }
    }
}