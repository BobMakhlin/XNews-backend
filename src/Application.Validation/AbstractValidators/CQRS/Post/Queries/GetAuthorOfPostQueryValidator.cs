using Application.CQRS.Posts.Queries;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Queries
{
    public class GetAuthorOfPostQueryValidator : AbstractValidator<GetAuthorOfPostQuery>
    {
        public GetAuthorOfPostQueryValidator()
        {
            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}