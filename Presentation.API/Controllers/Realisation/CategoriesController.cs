using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Commands;
using Application.CQRS.Categories.Models;
using Application.CQRS.Categories.Queries;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync([FromQuery] GetAllCategoriesQuery request)
        {
            try
            {
                IPagedList<CategoryDto> categories = await Mediator.Send(request);
                return Ok(categories);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync([FromRoute] Guid id)
        {
            CategoryDto categoryDto = await Mediator.Send(new GetCategoryByIdQuery {CategoryId = id});

            if (categoryDto == null)
            {
                return NotFound();
            }

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoryAsync([FromBody] CreateCategoryCommand request)
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
    }
}