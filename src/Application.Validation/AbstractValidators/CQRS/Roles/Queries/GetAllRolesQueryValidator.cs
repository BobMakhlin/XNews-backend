using Application.CQRS.Roles.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Roles.Queries
{
    public class GetAllRolesQueryValidator : AbstractValidator<GetAllRolesQuery>
    {
        public GetAllRolesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}