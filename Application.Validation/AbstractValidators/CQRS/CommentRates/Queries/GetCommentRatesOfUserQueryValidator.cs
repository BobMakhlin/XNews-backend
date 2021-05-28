using Application.CQRS.CommentRates.Queries;
using Application.Validation.AbstractValidators.Common;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.CommentRates.Queries
{
    public class GetCommentRatesOfUserQueryValidator : AbstractValidator<GetCommentRatesOfUserQuery>
    {
        public GetCommentRatesOfUserQueryValidator()
        {
            Include(new PaginationRequestValidator());
        }
    }
}