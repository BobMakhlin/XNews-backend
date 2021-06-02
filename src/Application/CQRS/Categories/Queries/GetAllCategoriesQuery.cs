using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Categories.Models;
using Application.Pagination.Common.Models;
using Application.Pagination.Common.Models.PagedList;
using Application.Pagination.Extensions;
using Application.Persistence.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.CQRS.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<IPagedList<CategoryDto>>, IPaginationRequest
    {
        #region IPaginationRequest

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<GetAllCategoriesQuery, IPagedList<CategoryDto>>
        {
            #region Fields

            private readonly IXNewsDbContext _context;
            private readonly IMapper _mapper;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            #endregion

            #region IRequestHandler<GetAllCategoriesQuery, IPagedList<CategoryDto>>

            public async Task<IPagedList<CategoryDto>> Handle(GetAllCategoriesQuery request,
                CancellationToken cancellationToken)
            {
                return await _context.Category
                    .OrderBy(c => c.CategoryId)
                    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ProjectToPagedListAsync(request, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}