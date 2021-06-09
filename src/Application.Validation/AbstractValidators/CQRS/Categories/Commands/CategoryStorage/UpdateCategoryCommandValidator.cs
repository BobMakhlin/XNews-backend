using Application.CQRS.Categories.Commands.CategoryStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Commands.CategoryStorage
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IXNewsDbContext context)
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty();

            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(CategoryValidationOptions.TitleMinLength, CategoryValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Category, c => c.Title);
        }
    }
}