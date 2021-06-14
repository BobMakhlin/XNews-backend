using Application.CQRS.Posts.Queries.PostCategory;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostCategory
{
    public class GetListOfPostCategoriesQueryValidator : AbstractValidator<GetListOfPostCategoriesQuery>
    {
        public GetListOfPostCategoriesQueryValidator()
        {
            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}