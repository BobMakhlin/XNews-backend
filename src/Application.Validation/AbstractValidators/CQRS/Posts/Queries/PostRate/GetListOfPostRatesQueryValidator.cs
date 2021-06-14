using Application.CQRS.Posts.Queries.PostRate;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostRate
{
    public class GetListOfPostRatesQueryValidator : AbstractValidator<GetListOfPostRatesQuery>
    {
        public GetListOfPostRatesQueryValidator()
        {
            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}