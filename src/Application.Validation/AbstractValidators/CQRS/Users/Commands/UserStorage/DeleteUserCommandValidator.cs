using Application.CQRS.Users.Commands.UserStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Commands.UserStorage
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}