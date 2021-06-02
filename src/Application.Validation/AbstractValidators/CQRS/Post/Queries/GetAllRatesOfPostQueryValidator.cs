using Application.CQRS.Posts.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Queries
{
    public class GetAllRatesOfPostQueryValidator : AbstractValidator<GetAllRatesOfPostQuery>
    {
        public GetAllRatesOfPostQueryValidator()
        {
            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}