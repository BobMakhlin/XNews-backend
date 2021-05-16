using Application.CQRS.Categories.Commands;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Categories.Commands
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(CategoryValidationOptions.TitleMinLength, CategoryValidationOptions.TitleMaxLength);
        }
    }
}