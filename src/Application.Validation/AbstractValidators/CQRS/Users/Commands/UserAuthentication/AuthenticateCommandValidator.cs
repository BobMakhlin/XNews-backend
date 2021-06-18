using Application.CQRS.Users.Commands.UserAuthentication;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands.UserAuthentication
{
    public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
    {
        public AuthenticateCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}