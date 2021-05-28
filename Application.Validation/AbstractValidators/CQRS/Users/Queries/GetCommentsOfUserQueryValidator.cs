using Application.CQRS.Users.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Users.Queries
{
    public class GetCommentsOfUserQueryValidator : AbstractValidator<GetCommentsOfUserQuery>
    {
        public GetCommentsOfUserQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}