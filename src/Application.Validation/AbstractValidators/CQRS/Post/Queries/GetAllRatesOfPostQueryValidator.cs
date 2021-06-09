using Application.CQRS.Posts.Queries;
using Application.CQRS.Posts.Queries.PostRate;
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