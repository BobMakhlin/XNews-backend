using Application.CQRS.Users.Queries.UserStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserStorage
{
    public class GetPagedListOfUsersQueryValidator : AbstractValidator<GetPagedListOfUsersQuery>
    {
        public GetPagedListOfUsersQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}