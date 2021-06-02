using Application.Pagination.Common.Models;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.AbstractValidators.Common
{
    public class PaginationRequestValidator : AbstractValidator<IPaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(q => q.PageNumber)
                .GreaterThanOrEqualTo(PaginationValidationOptions.MinPageNumber);

            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(PaginationValidationOptions.MinPageSize)
                .LessThanOrEqualTo(PaginationValidationOptions.MaxPageSize);
        }
    }
}