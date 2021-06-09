using Application.CQRS.Categories.Queries.CategoryStorage;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Queries.CategoryStorage
{
    public class GetAllCategoriesQueryValidator : AbstractValidator<GetAllCategoriesQuery>
    {
        public GetAllCategoriesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}