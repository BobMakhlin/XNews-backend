using Application.CQRS.Users.Queries.UserComment;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries.UserComment
{
    public class GetPagedListOfUserCommentsQueryValidator : AbstractValidator<GetPagedListOfUserCommentsQuery>
    {
        public GetPagedListOfUserCommentsQueryValidator()
        {
            Include(new PaginationRequestValidator());

            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}