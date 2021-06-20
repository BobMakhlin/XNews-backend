using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Database;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Commands.UserAuthentication
{
    public class RevokeTokenCommand : IRequest
    {
        #region Properties

        public string RefreshToken { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<RevokeTokenCommand>
        {
            #region Fields

            private readonly IXNewsIdentityDbContextSimplified _identityDbContext;
            private readonly IJwtService<ApplicationUser, string, RefreshToken> _jwtService;

            #endregion

            #region Constructors

            public Handler(IJwtService<ApplicationUser, string, RefreshToken> jwtService,
                IXNewsIdentityDbContextSimplified identityDbContext)
            {
                _jwtService = jwtService;
                _identityDbContext = identityDbContext;
            }

            #endregion

            #region IRequestHandler<RevokeTokenCommand>

            public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
            {
                RefreshToken refreshToken =
                    await _identityDbContext.RefreshTokens
                        .SingleOrDefaultAsync(rf => rf.Token == request.RefreshToken, cancellationToken)
                        .ConfigureAwait(false)
                    ?? throw new NotFoundException(nameof(Identity.Entities.RefreshToken), request.RefreshToken);

                IIdentityResult identityResult = await _jwtService.RevokeRefreshTokenAsync(refreshToken)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}