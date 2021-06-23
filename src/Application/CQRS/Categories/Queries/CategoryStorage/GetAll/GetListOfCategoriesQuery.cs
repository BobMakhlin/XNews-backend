using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Extensions;
using Application.CQRS.Categories.Models;
using Application.Persistence.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Categories.Queries.CategoryStorage.GetAll
{
    public class GetListOfCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
    {
        #region Classes

        public class Handler : IRequestHandler<GetListOfCategoriesQuery, IEnumerable<CategoryDto>>
        {
            #region Fields

            private readonly IMapper _mapper;
            private readonly IXNewsDbContext _context;

            #endregion

            #region Constructors

            public Handler(IMapper mapper, IXNewsDbContext context)
            {
                _mapper = mapper;
                _context = context;
            }

            #endregion
            
            #region IRequestHandler<GetListOfCategoriesQuery, IEnumerable<CategoryDto>>

            public async Task<IEnumerable<CategoryDto>> Handle(GetListOfCategoriesQuery request, CancellationToken cancellationToken)
            {
                return await _context.Category
                    .AsNoTracking()
                    .ProjectToListAsync<CategoryDto>(_mapper.ConfigurationProvider, cancellationToken)
                    .ConfigureAwait(false);
            }

            #endregion
        }

        #endregion
    }
}