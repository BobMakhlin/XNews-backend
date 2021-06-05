using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands
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