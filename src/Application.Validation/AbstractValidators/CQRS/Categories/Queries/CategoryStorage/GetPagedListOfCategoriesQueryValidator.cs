using Application.CQRS.Categories.Queries.CategoryStorage;
using Application.CQRS.Categories.Queries.CategoryStorage.GetAll;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Queries.CategoryStorage
{
    public class GetPagedListOfCategoriesQueryValidator : AbstractValidator<GetPagedListOfCategoriesQuery>
    {
        public GetPagedListOfCategoriesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}