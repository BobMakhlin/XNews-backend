using Application.CQRS.Categories.Commands;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using Application.Validation.Tools.Helpers;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Commands
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator(IXNewsDbContext context)
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(CategoryValidationOptions.TitleMinLength, CategoryValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Category, c => c.Title);
        }
    }
}