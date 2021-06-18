using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Commands.CategoryStorage;
using Application.CQRS.Categories.Models;
using Application.CQRS.Categories.Queries.CategoryStorage.GetAll;
using Application.CQRS.Categories.Queries.CategoryStorage.GetById;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : MyBaseController
    {
        #region CategoryStorage

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetPagedListOfCategoriesAsync()
        {
            IEnumerable<CategoryDto> categories = await Mediator.Send(new GetListOfCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("paged-list")]
        public async Task<ActionResult<IPagedList<CategoryDto>>> GetListOfCategoriesAsync(
            [FromQuery] GetPagedListOfCategoriesQuery request)
        {
            IPagedList<CategoryDto> categories = await Mediator.Send(request);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync([FromRoute] Guid id)
        {
            CategoryDto categoryDto = await Mediator.Send(new GetCategoryByIdQuery {CategoryId = id});
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> PostCategoryAsync([FromBody] CreateCategoryCommand request)
        {
            Guid createdCategoryId = await Mediator.Send(request);
            return Ok(createdCategoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryAsync([FromRoute] Guid id, [FromBody] UpdateCategoryCommand request)
        {
            if (id != request.CategoryId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCategoryCommand {CategoryId = id});
            return NoContent();
        }

        #endregion
    }
}