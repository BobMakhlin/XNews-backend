using Application.CQRS.Categories.Queries;
using Application.Validation.Validators.Common;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Categories.Queries
{
    public class GetAllCategoriesQueryValidator : AbstractValidator<GetAllCategoriesQuery>
    {
        public GetAllCategoriesQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}