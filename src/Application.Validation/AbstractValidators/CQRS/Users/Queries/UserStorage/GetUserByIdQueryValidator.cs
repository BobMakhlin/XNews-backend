using Application.CQRS.Users.Queries.UserStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserStorage
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}