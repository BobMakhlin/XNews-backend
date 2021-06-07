using Application.CQRS.PostRates.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.PostRates.Queries
{
    public class GetAuthorOfPostRateQueryValidator : AbstractValidator<GetAuthorOfPostRateQuery>
    {
        public GetAuthorOfPostRateQueryValidator()
        {
            RuleFor(q => q.PostRateId)
                .NotEmpty();
        }
    }
}