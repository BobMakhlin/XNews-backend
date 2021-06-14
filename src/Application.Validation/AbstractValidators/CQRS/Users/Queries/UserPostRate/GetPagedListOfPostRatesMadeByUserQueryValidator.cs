using Application.CQRS.Users.Queries.UserPostRate;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserPostRate
{
    public class GetPagedListOfPostRatesMadeByUserQueryValidator : AbstractValidator<GetPagedListOfPostRatesMadeByUserQuery>
    {
        public GetPagedListOfPostRatesMadeByUserQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}