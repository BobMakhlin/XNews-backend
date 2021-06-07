using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.CQRS.Users.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Posts.Queries
{
    public class GetAuthorOfPostQuery : IRequest<UserDto>
    {
        #region Properties

        public Guid PostId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetAuthorOfPostQuery, UserDto>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IXNewsDbContext _context;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IIdentityStorage<ApplicationUser, string> userStorage,
                IMapper mapper)
            {
                _context = context;
                _userStorage = userStorage;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetAuthorOfPostQuery, UserDto>

            public async Task<UserDto> Handle(GetAuthorOfPostQuery request, CancellationToken cancellationToken)
            {
                string postAuthorId = await _context.Post
                                          .Where(p => p.PostId == request.PostId)
                                          .Select(p => p.UserId)
                                          .SingleOrDefaultAsync(cancellationToken)
                                          .ConfigureAwait(false)
                                      ?? throw new NotFoundException();

                return await _userStorage
                           .GetAll()
                           .Where(u => u.Id == postAuthorId)
                           .ProjectToSingleOrDefaultAsync<UserDto>(_mapper.ConfigurationProvider, cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException();
            }

            #endregion
        }

        #endregion
    }
}