using Application.CQRS.Users.Queries;
using Application.CQRS.Users.Queries.UserCommentRate;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries
{
    public class GetCommentRatesOfUserQueryValidator : AbstractValidator<GetCommentRatesOfUserQuery>
    {
        public GetCommentRatesOfUserQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}