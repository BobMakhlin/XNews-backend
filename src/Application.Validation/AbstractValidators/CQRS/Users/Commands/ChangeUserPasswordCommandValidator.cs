using Application.CQRS.Users.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands
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