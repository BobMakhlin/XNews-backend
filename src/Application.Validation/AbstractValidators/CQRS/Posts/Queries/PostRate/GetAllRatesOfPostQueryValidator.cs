using Application.CQRS.Posts.Queries.PostRate;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostRate
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