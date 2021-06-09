using Application.CQRS.Comments.Commands.CommentStorage;
using Application.Validation.Options;
using FluentValidation;

namespace Application.Validation.AbstractValidators.CQRS.Comments.Commands.CommentStorage
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator()
        {
            RuleFor(c => c.CommentId)
                .NotEmpty();
            
            RuleFor(c => c.Content)
                .NotEmpty()
                .Length(CommentValidationOptions.ContentMinLength, CommentValidationOptions.ContentMaxLength);
            
            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}