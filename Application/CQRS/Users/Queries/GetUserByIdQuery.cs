using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Users.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using AutoMapper;
using MediatR;

namespace Application.CQRS.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string UserId { get; set; }

        public class Handler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            #region Fields

            private readonly IIdentityUserService<ApplicationUser, ApplicationRole> _userService;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IIdentityUserService<ApplicationUser, ApplicationRole> userService, IMapper mapper)
            {
                _userService = userService;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetUserByIdQuery, UserDto>

            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                ApplicationUser user = await _userService.FindByIdAsync(request.UserId)
                    .ConfigureAwait(false);
                return _mapper.Map<UserDto>(user);
            }

            #endregion
        }
    }
}