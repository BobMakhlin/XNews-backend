using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.CQRS.Comments.Models;
using Application.Identity.Extensions;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries.UserComment
{
    public class GetCommentsOfUserQuery : IRequest<IPagedList<CommentDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationInfo

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetCommentsOfUserQuery, IPagedList<CommentDto>>
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

            #region IRequestHandler<GetCommentsOfUserQuery, IPagedList<CommentDto>>

            public async Task<IPagedList<CommentDto>> Handle(GetCommentsOfUserQuery request,
                CancellationToken cancellationToken)
            {
                IPagedList<CommentDto> comments = await _context.Comment
                    .Where(c => c.UserId == request.UserId)
                    .OrderBy(c => c.CommentId)
                    .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (comments.TotalItemsCount > 0)
                {
                    return comments;
                }

                bool userExists = await _userStorage.Exists(request.UserId)
                    .ConfigureAwait(false);
                return userExists
                    ? PagedList<CommentDto>.CreateEmptyPagedList(request)
                    : throw new NotFoundException();
            }

            #endregion
        }

        #endregion
    }
}