using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.CommentRates.Models;
using Application.Identity.Interfaces;
using Application.Identity.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Users.Queries.UserCommentRate
{
    public class GetPagedListOfCommentRatesMadeByUserQuery : IRequest<IPagedList<CommentRateDto>>, IPaginationRequest
    {
        #region Properties

        public string UserId { get; set; }

        #endregion

        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetPagedListOfCommentRatesMadeByUserQuery, IPagedList<CommentRateDto>>
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

            #region IRequestHandler<GetPagedListOfCommentRatesMadeByUserQuery, IPagedList<CommentRateDto>>

            public async Task<IPagedList<CommentRateDto>> Handle(GetPagedListOfCommentRatesMadeByUserQuery request,
                CancellationToken cancellationToken)
            {
                IPagedList<CommentRateDto> commentRates = await _context.CommentRate
                    .Where(cr => cr.UserId == request.UserId)
                    .OrderBy(c => c.CommentRateId)
                    .ProjectTo<CommentRateDto>(_mapper.ConfigurationProvider)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);

                if (commentRates.TotalItemsCount == 0)
                {
                    await _userStorage.ThrowIfDoesNotExistAsync(request.UserId)
                        .ConfigureAwait(false);
                }
                
                return commentRates;
            }

            #endregion
        }

        #endregion
    }
}