using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Interfaces.Storages;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Commands.UserAuthentication
{
    public class AuthenticateCommand : IRequest<AuthenticationResponse>
    {
        #region Properties

        public string Email { get; set; }
        public string Password { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<AuthenticateCommand, AuthenticationResponse>
        {
            #region Fields

            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;
            private readonly IJwtService<ApplicationUser, string, RefreshToken> _jwtService;

            #endregion

            #region Constructors

            public Handler(IJwtService<ApplicationUser, string, RefreshToken> jwtService,
                IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _jwtService = jwtService;
                _userStorage = userStorage;
            }

            #endregion

            #region IRequestHandler<AuthenticateCommand, AuthenticationResponse>

            public async Task<AuthenticationResponse> Handle(AuthenticateCommand request,
                CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userStorage.GetAll()
                                           .Where(x => x.Email == request.Email)
                                           .Include(x => x.RefreshTokens)
                                           .SingleOrDefaultAsync(cancellationToken)
                                       ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

                (IIdentityResult, AuthenticationResponse) result = await _jwtService
                    .AuthenticateAsync(user, request.Password)
                    .ConfigureAwait(false);
                result.Item1.ThrowIfFailed();

                return result.Item2;
            }

            #endregion
        }

        #endregion
    }
}