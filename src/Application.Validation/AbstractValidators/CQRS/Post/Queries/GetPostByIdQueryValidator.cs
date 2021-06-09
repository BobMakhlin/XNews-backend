using Application.CQRS.Posts.Queries;
using Application.CQRS.Posts.Queries.PostStorage;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Post.Queries
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