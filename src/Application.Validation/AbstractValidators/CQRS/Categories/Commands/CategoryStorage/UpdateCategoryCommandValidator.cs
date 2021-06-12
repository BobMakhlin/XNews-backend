using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.CQRS.Categories.Commands.CategoryStorage;
using Application.Persistence.Interfaces;
using Application.Validation.Options;
using Application.Validation.Tools.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Validation.AbstractValidators.CQRS.Categories.Commands.CategoryStorage
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        #region Fields

        private readonly IXNewsDbContext _context;

        #endregion

        #region Constructors

        public UpdateCategoryCommandValidator(IXNewsDbContext context)
        {
            _context = context;
            
            RuleFor(c => c.CategoryId)
                .NotEmpty();

            RuleFor(c => c.Title)
                .NotEmpty()
                .Length(CategoryValidationOptions.TitleMinLength, CategoryValidationOptions.TitleMaxLength)
                .UniqueInsideOfDbSetColumn(context.Category, c => c.Title)
                .WhenAsync(CategoryTitleWasUpdated);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the category title was changed in the current update.
        /// </summary>
        /// <param name="command">
        /// Contains information about the current update of the category.
        /// </param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> CategoryTitleWasUpdated(UpdateCategoryCommand command, CancellationToken token)
        {
            string currentCategoryTitle = await _context.Category
                .Where(c => c.CategoryId == command.CategoryId)
                .Select(c => c.Title)
                .SingleOrDefaultAsync(token);
            return currentCategoryTitle != command.Title;
        } 
        
        #endregion
    }
}