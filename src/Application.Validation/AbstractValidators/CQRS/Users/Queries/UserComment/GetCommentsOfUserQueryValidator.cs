using Application.CQRS.Users.Queries.UserComment;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserComment
{
    public class GetCommentsOfUserQueryValidator : AbstractValidator<GetCommentsOfUserQuery>
    {
        public GetCommentsOfUserQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}