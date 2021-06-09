using Application.CQRS.Posts.Queries.PostCategory;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostCategory
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