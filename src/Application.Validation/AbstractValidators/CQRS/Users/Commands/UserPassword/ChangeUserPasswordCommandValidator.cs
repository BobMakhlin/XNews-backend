using Application.CQRS.Users.Commands.UserPassword;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands.UserPassword
{
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}