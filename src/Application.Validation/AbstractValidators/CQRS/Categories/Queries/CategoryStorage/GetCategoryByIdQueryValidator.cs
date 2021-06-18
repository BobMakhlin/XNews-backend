using Application.CQRS.Categories.Queries.CategoryStorage.GetById;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Queries.CategoryStorage
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(q => q.CategoryId)
                .NotEmpty();
        }
    }
}