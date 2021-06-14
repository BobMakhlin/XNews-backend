using Application.CQRS.Users.Queries.UserCommentRate;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserCommentRate
{
    public class GetPagedListOfCommentRatesMadeByUserQueryValidator : AbstractValidator<GetPagedListOfCommentRatesMadeByUserQuery>
    {
        public GetPagedListOfCommentRatesMadeByUserQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}