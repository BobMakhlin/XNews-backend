using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        public class Handler : IRequestHandler<UpdateCategoryCommand>
        {
            #region Fields

            private readonly IXNewsDbContext _context;

            #endregion

            #region Constructors

            public Handler(IXNewsDbContext context)
            {
                _context = context;
            }

            #endregion

            #region IRequestHandler<UpdateCategoryCommand>

            public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                Category category = await _context.Category.FindAsync(request.CategoryId)
                    .ConfigureAwait(false);
                if (category == null)
                {
                    throw new NotFoundException();
                }

                UpdateCategoryProperties(category, request);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Updates <paramref name="category"/> properties using the <paramref name="request"/> parameter.
            /// </summary>
            /// <param name="category">An object which properties will be updated</param>
            /// <param name="request">An object that contains new properties values for <paramref name="category"/> parameter</param>
            private void UpdateCategoryProperties(Category category, UpdateCategoryCommand request)
            {
                category.Title = request.Title;
            }

            #endregion
        }
    }
}