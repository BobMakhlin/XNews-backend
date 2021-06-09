using Application.CQRS.Posts.Commands.PostCategory;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Commands.PostCategory
{
    public class AddCategoryToPostCommandValidator : AbstractValidator<AddCategoryToPostCommand>
    {
        public AddCategoryToPostCommandValidator()
        {
            RuleFor(c => c.PostId)
                .NotEmpty();

            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}