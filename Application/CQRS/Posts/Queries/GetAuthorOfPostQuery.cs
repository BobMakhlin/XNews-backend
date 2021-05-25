using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
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
        public Guid PostId { get; set; }

        public class Handler : IRequestHandler<GetAuthorOfPostQuery, UserDto>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IIdentityUserService<ApplicationUser> _userService;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IIdentityUserService<ApplicationUser> userService, IXNewsDbContext context, IMapper mapper)
            {
                _userService = userService;
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetAuthorOfPostQuery, UserDto>

            public async Task<UserDto> Handle(GetAuthorOfPostQuery request, CancellationToken cancellationToken)
            {
                string authorId = await GetAuthorIdOfPostAsync(request.PostId, cancellationToken)
                    .ConfigureAwait(false);
                ApplicationUser applicationUser = await GetUserByIdAsync(authorId)
                    .ConfigureAwait(false);

                return _mapper.Map<UserDto>(applicationUser);
            }

            #endregion

            #region Methods

            /// <summary>
            /// Returns the author id of a post with the specified <paramref name="postId"/>.
            /// </summary>
            /// <param name="postId"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            /// <exception cref="NotFoundException">
            /// When a post with the specified <paramref name="postId"/> was not found
            /// </exception>
            private async Task<string> GetAuthorIdOfPostAsync(Guid postId, CancellationToken cancellationToken)
            {
                return await _context.Post
                           .Where(p => p.PostId == postId)
                           .Select(p => p.UserId)
                           .SingleOrDefaultAsync(cancellationToken)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException();
            }

            /// <summary>
            /// Finds the user with the specified <paramref name="userId"/>.
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>
            /// <exception cref="NotFoundException">
            /// When a user with the specified <paramref name="userId"/> was not found
            /// </exception>
            private async Task<ApplicationUser> GetUserByIdAsync(string userId)
            {
                return await _userService.FindByIdAsync(userId)
                           .ConfigureAwait(false)
                       ?? throw new NotFoundException();
            }

            #endregion
        }
    }
}