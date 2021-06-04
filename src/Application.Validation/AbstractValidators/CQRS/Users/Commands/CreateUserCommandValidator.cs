using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}