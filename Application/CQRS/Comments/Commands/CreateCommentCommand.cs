using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Comments.Commands
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }

        public class Handler : IRequestHandler<CreateCommentCommand, Guid>
        {
            #region Fields

            private readonly IXNewsDbContext _context;

            #endregion
            
            #region Constructors

            public Handler(IXNewsDbContext context)
            {
                _context = context;
            }

            #endregion
            
            #region IRequestHandler<CreateCommentCommand, Guid>

            public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                Comment comment = ConvertToComment(request);

                await CreateCommentAsync(comment, cancellationToken)
                    .ConfigureAwait(false);

                return comment.CommentId;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="Comment"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="Comment"/></returns>
            private Comment ConvertToComment(CreateCommentCommand command)
            {
                return new()
                {
                    Content = command.Content,
                    PostId = command.PostId,
                    ParentCommentId = command.ParentCommentId
                };
            }

            private async Task CreateCommentAsync(Comment comment, CancellationToken cancellationToken)
            {
                await _context.Comment.AddAsync(comment, cancellationToken)
                    .ConfigureAwait(false);

                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}
