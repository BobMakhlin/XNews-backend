﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using MediatR;

namespace Application.CQRS.Categories.Commands.CategoryStorage
{
    public class DeleteCategoryCommand : IRequest
    {
        #region Properties

        public Guid CategoryId { get; set; }

        #endregion

        #region Classes

        public class Handler : IRequestHandler<DeleteCategoryCommand>
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
            
            #region IRequestHandler<DeleteCategoryCommand>

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                Category category = await _context.Category.FindAsync(request.CategoryId)
                                        .ConfigureAwait(false)
                                    ?? throw new NotFoundException(nameof(Category), request.CategoryId);

                _context.Category.Remove(category);
                await _context.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return Unit.Value;
            }

            #endregion
        }

        #endregion
    }
}