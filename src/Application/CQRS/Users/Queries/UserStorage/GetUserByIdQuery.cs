using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Users.Models;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Queries.UserStorage
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetUserByIdQuery, UserDto>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IIdentityStorage<ApplicationUser, string> userStorage, IMapper mapper)
            {
                _userStorage = userStorage;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetUserByIdQuery, UserDto>

            public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
            {
                return await _userStorage
                           .GetAll()
                           .AsNoTracking()
                           .Where(u => u.Id == request.UserId)
                           .ProjectToSingleOrDefaultAsync<UserDto>(_mapper.ConfigurationProvider, cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException(nameof(ApplicationUser), request.UserId);
            }

            #endregion
        }

        #endregion
    }
}