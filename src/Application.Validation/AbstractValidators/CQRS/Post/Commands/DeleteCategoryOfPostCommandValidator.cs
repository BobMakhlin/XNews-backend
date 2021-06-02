using Application.CQRS.Posts.Commands;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Commands
{
    public class DeleteCategoryOfPostCommandValidator : AbstractValidator<DeleteCategoryOfPostCommand>
    {
        public DeleteCategoryOfPostCommandValidator()
        {
            RuleFor(c => c.PostId)
                .NotEmpty();

            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }   
    }
}