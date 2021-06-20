using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Database;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Commands.UserAuthentication
{
    public class RefreshSessionCommand : IRequest<AuthenticationResponse>
    {
        #region Properties

        public string RefreshToken { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<RefreshSessionCommand, AuthenticationResponse>
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

            #region IRequestHandler<RefreshSessionCommand, AuthenticationResponse>

            public async Task<AuthenticationResponse> Handle(RefreshSessionCommand request,
                CancellationToken cancellationToken)
            {
                RefreshToken refreshToken = 
                    await _identityDbContext.RefreshTokens
                        .SingleOrDefaultAsync(rf => rf.Token == request.RefreshToken, cancellationToken)
                        .ConfigureAwait(false)
                    ?? throw new NotFoundException(nameof(Identity.Entities.RefreshToken), request.RefreshToken);

                (IIdentityResult, AuthenticationResponse) result = await _jwtService
                    .RefreshSessionAsync(refreshToken)
                    .ConfigureAwait(false);
                result.Item1.ThrowIfFailed();

                return result.Item2;
            }

            #endregion
        }

        #endregion
    }
}