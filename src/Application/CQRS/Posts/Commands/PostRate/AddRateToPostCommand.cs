﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Posts.Commands.PostRate
{
    public class AddRateToPostCommand : IRequest<Guid>
    {
        #region Properties

        public Guid PostId { get; set; }
        public double Rate { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<AddRateToPostCommand, Guid>
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
            
            #region IRequestHandler<AddRateToPostCommand, Guid>

            public async Task<Guid> Handle(AddRateToPostCommand request, CancellationToken cancellationToken)
            {
                await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                    .ConfigureAwait(false);
                
                Domain.Primary.Entities.PostRate postRate = ConvertToPostRate(request);
                
                await CreatePostRateAsync(postRate, cancellationToken)
                    .ConfigureAwait(false);
                
                return postRate.PostRateId;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="PostRate"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <param name="command"></param>
            /// <returns>The created object of type <see cref="PostRate"/></returns>
            private Domain.Primary.Entities.PostRate ConvertToPostRate(AddRateToPostCommand command)
            {
                return new()
                {
                    PostId = command.PostId,
                    Rate = command.Rate,
                    UserId = command.UserId
                };
            }

            private async Task CreatePostRateAsync(Domain.Primary.Entities.PostRate postRate, CancellationToken cancellationToken)
            {
                await _context.PostRate.AddAsync(postRate, cancellationToken)
                    .ConfigureAwait(false);
                
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}