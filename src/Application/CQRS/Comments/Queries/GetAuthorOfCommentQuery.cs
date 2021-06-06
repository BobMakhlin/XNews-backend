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

namespace Application.CQRS.Comments.Queries
{
    public class GetAuthorOfCommentQuery : IRequest<UserDto>
    {
        #region Properties

        public Guid CommentId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetAuthorOfCommentQuery, UserDto>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IXNewsDbContext _context;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IIdentityStorage<ApplicationUser, string> userStorage, IMapper mapper)
            {
                _context = context;
                _userStorage = userStorage;
                _mapper = mapper;
            }

            #endregion
            
            #region IRequestHandler<GetAuthorOfCommentQuery, UserDto>

            public async Task<UserDto> Handle(GetAuthorOfCommentQuery request, CancellationToken cancellationToken)
            {
                string commentAuthorId = await _context.Comment
                                          .Where(p => p.CommentId == request.CommentId)
                                          .Select(p => p.UserId)
                                          .SingleOrDefaultAsync(cancellationToken)
                                          .ConfigureAwait(false)
                                      ?? throw new NotFoundException();

                return await _userStorage
                           .GetAll()
                           .Where(u => u.Id == commentAuthorId)
                           .ProjectToSingleOrDefaultAsync<UserDto>(_mapper.ConfigurationProvider, cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException();
            }

            #endregion
        }

        #endregion
    }
}