﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Comments.Commands.CommentStorage
{
    public class UpdateCommentCommand : IRequest
    {
        #region Properties

        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<UpdateCommentCommand>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion
            
            #region Constructors

            public Handler(IXNewsDbContext context, IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _context = context;
                _userStorage = userStorage;
            }

            #endregion
            
            #region IRequestHandler<UpdateCommentCommand>

            public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                    .ConfigureAwait(false);
                
                Comment comment = await _context.Comment.FindAsync(request.CommentId)
                                      .ConfigureAwait(false)
                                  ?? throw new NotFoundException(nameof(Comment), request.CommentId);

                UpdateCommentProperties(comment, request);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Updates <paramref name="comment"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="comment">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="comment"/> parameter</param>
            private void UpdateCommentProperties(Comment comment, UpdateCommentCommand request)
            {
                comment.Content = request.Content;
                comment.UserId = request.UserId;
            }

            #endregion
        }

        #endregion
    }
}