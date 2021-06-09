using Application.CQRS.Categories.Queries;
using Application.CQRS.Categories.Queries.CategoryStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Queries
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