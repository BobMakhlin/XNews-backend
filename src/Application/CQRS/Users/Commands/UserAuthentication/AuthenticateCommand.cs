using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces.JWT;
using Application.Identity.Models;
using Application.Identity.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

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

            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IJwtService<ApplicationUser, string> _jwtService;

            #endregion

            #region Constructors

            public Handler(UserManager<ApplicationUser> userManager,
                IJwtService<ApplicationUser, string> jwtService)
            {
                _userManager = userManager;
                _jwtService = jwtService;
            }

            #endregion

            #region IRequestHandler<AuthenticateCommand, AuthenticationResponse>

            public async Task<AuthenticationResponse> Handle(AuthenticateCommand request,
                CancellationToken cancellationToken)
            {
                ApplicationUser user =
                    await _userManager.FindByEmailAsync(request.Email)
                        .ConfigureAwait(false)
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