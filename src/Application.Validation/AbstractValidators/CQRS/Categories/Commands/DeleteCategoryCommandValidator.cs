using Application.CQRS.Categories.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Commands
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