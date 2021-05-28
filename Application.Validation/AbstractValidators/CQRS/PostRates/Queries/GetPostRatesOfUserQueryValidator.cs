using Application.CQRS.PostRates.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.PostRates.Queries
{
    public class GetPostRatesOfUserQueryValidator : AbstractValidator<GetPostRatesOfUserQuery>
    {
        public GetPostRatesOfUserQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}