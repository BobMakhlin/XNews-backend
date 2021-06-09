using Application.CQRS.Roles.Queries.RoleStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Queries.RoleStorage
{
    public class GetAllRolesQueryValidator : AbstractValidator<GetAllRolesQuery>
    {
        public GetAllRolesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}