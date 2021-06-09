using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Posts.Commands.PostStorage
{
    public class UpdatePostCommand : IRequest
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }

        public class Handler : IRequestHandler<UpdatePostCommand>
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

            #region IRequestHandler<UpdatePostCommand>

            public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
            {
                Post post = await _context.Post.FindAsync(request.PostId)
                                .ConfigureAwait(false)
                            ?? throw new NotFoundException();

                UpdatePostProperties(post, request);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Updates <paramref name="post"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="post">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="post"/> parameter</param>
            private void UpdatePostProperties(Post post, UpdatePostCommand request)
            {
                post.Title = request.Title;
                post.Content = request.Content;
                post.UserId = request.UserId;
            }

            #endregion
        }
    }
}