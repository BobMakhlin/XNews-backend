using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.Identity.Entities;
using Application.Identity.Interfaces;
using Application.Identity.Results;
using MediatR;

namespace Application.CQRS.Users.Commands.UserStorage
{
    public class CreateUserCommand : IRequest<string>
    {
        #region Properties

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<CreateUserCommand, string>
        {
            #region Fields

            private readonly IUserPasswordService<ApplicationUser, string> _userPasswordService;

            #endregion

            #region Constructors

            public Handler(IUserPasswordService<ApplicationUser, string> userPasswordService)
            {
                _userPasswordService = userPasswordService;
            }

            #endregion

            #region IRequestHandler<CreateUserCommand>

            public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                ApplicationUser user = ConvertToApplicationUser(request);

                IIdentityResult identityResult = await _userPasswordService
                    .CreateUserWithPasswordAsync(user, request.Password)
                    .ConfigureAwait(false);
                identityResult.ThrowIfFailed();

               return user.Id;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Creates an object of type <see cref="ApplicationUser"/> based on the given <paramref name="command"/>.
            /// </summary>
            /// <returns>The created object of type <see cref="ApplicationUser"/></returns>
            private ApplicationUser ConvertToApplicationUser(CreateUserCommand command)
            {
                return new()
                {
                    UserName = command.UserName,
                    Email = command.Email,
                    PhoneNumber = command.PhoneNumber
                };
            }

            #endregion
        }

        #endregion
    }
}