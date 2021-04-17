using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Commands;
using Application.CQRS.Categories.Models;
using Application.CQRS.Categories.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryDto> categories = await Mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(Guid id)
        {
            CategoryDto categoryDto = await Mediator.Send(new GetCategoryByIdQuery {CategoryId = id});

            if (categoryDto == null)
            {
                return NotFound();
            }

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoryAsync(CreateCategoryCommand request)
        {
            Guid createdCategoryId = await Mediator.Send(request);
            return Ok(createdCategoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryAsync(Guid id, UpdateCategoryCommand request)
        {
            if (id != request.CategoryId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            await Mediator.Send(new DeleteCategoryCommand {CategoryId = id});
            return NoContent();
        }
    }
}