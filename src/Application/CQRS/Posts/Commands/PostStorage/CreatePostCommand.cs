using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Posts.Commands.PostStorage
{
    public class CreatePostCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }

        public class Handler : IRequestHandler<CreatePostCommand, Guid>
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
            
            #region IRequestHandler<CreatePostCommand, Guid>

            public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                Post post = ConvertToPost(request);
                
                await CreatePostAsync(post, cancellationToken)
                    .ConfigureAwait(false);
                
                return post.PostId;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="Post"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="Post"/></returns>
            private Post ConvertToPost(CreatePostCommand command)
            {
                return new()
                {
                    Title = command.Title,
                    Content = command.Content,
                    UserId = command.UserId
                };
            }

            private async Task CreatePostAsync(Post post, CancellationToken cancellationToken)
            {
                await _context.Post.AddAsync(post, cancellationToken)
                    .ConfigureAwait(false);
                
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }
    }
}