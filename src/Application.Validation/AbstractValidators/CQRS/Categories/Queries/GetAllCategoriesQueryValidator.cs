using Application.CQRS.Categories.Queries;
using Application.CQRS.Categories.Queries.CategoryStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Queries
{
    public class GetAllCategoriesQueryValidator : AbstractValidator<GetAllCategoriesQuery>
    {
        public GetAllCategoriesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}