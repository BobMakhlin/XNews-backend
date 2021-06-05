using Application.CQRS.Users.Commands.UserStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}