using Application.CQRS.Categories.Commands.CategoryStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Commands.CategoryStorage
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}