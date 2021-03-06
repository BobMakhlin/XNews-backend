using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Posts.Models;
using Application.Identity.Entities;
using Application.Identity.Interfaces.Storages;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Users.Queries.UserPost
{
    public class GetPagedListOfPostsMadeByUserQuery : IRequest<IPagedList<PostDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; } 

        #endregion
        
        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfPostsMadeByUserQuery, IPagedList<PostDto>>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IXNewsDbContext _context;
            private readonly IIdentityStorage<ApplicationUser, string> _userStorage;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper,
                IIdentityStorage<ApplicationUser, string> userStorage)
            {
                _context = context;
                _mapper = mapper;
                _userStorage = userStorage;
            }

            #endregion

            #region IRequestHandler<GetPagedListOfPostsMadeByUserQuery, IPagedList<PostDto>>

            public async Task<IPagedList<PostDto>> Handle(GetPagedListOfPostsMadeByUserQuery request,
                CancellationToken cancellationToken)
            {
                IPagedList<PostDto> posts = await _context.Post
                    .AsNoTracking()
                    .Where(p => p.UserId == request.UserId)
                    .OrderBy(p => p.PostId)
                    .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (posts.TotalItemsCount == 0)
                {
                    await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                        .ConfigureAwait(false);
                }

                return posts;
            }

            #endregion
        }

        #endregion
    }
}