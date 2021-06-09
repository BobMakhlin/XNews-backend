using Application.CQRS.Posts.Queries.PostStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Posts.Queries.PostStorage
{
    public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
    {
        public GetPostByIdQueryValidator()
        {
            RuleFor(q => q.PostId)
                .NotEmpty();
        }
    }
}