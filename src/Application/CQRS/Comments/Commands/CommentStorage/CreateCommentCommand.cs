using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Comments.Commands.CommentStorage
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        #region Properties

        public string Content { get; set; }
        public Guid PostId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<CreateCommentCommand, Guid>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;
            private readonly IXNewsDbContext _context;

            #endregion
            
            #region Constructors

            public Handler(IXNewsDbContext context, IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _context = context;
                _userStorage = userStorage;
            }

            #endregion
            
            #region IRequestHandler<CreateCommentCommand, Guid>

            public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                    .ConfigureAwait(false);
                
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
                    ParentCommentId = command.ParentCommentId,
                    UserId = command.UserId
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

        #endregion
    }
}
