using Application.CQRS.Users.Queries;
using Application.CQRS.Users.Queries.UserStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries
{
    public class GetAllUsersQueryValidator : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}