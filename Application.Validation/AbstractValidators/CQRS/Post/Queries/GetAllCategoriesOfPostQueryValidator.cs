using Application.CQRS.Posts.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Queries
{
    public class GetAllCategoriesOfPostQueryValidator : AbstractValidator<GetAllCategoriesOfPostQuery>
    {
        public GetAllCategoriesOfPostQueryValidator()
        {
            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}