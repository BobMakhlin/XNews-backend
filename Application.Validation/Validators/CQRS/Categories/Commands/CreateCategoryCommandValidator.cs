using Application.CQRS.Categories.Commands;
using Application.Persistence.Interfaces;
using Application.Validation.Helpers;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.Validators.CQRS.Categories.Commands
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IXNewsDbContext context)
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(CategoryValidationOptions.TitleMinLength, CategoryValidationOptions.TitleMaxLength)
                .MustAsync
                (
                    (title, token) => EfCoreValidationHelpers.IsColumnUniqueInsideOfDbSetAsync
                        (context.Category, c => c.Title, title, token)
                )
                .WithMessage("{PropertyName} must be unique");
        }
    }
}