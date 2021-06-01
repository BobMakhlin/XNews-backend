using Application.CQRS.Posts.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Commands
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