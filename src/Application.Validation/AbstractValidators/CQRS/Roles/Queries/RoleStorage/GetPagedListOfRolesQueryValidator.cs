using Application.CQRS.Roles.Queries.RoleStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Queries.RoleStorage
{
    public class GetPagedListOfRolesQueryValidator : AbstractValidator<GetPagedListOfRolesQuery>
    {
        public GetPagedListOfRolesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}