using Application.CQRS.Users.Commands.UserRole;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands.UserRole
{
    public class AddRoleToUserCommandValidator : AbstractValidator<AddRoleToUserCommand>
    {
        public AddRoleToUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();
            
            RuleFor(c => c.RoleId)
                .NotEmpty();
        }
    }
}