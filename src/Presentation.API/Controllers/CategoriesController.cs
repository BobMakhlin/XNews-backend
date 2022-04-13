using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Commands.CategoryStorage;
using Application.CQRS.Categories.Models;
using Application.CQRS.Categories.Queries.CategoryPost;
using Application.CQRS.Categories.Queries.CategoryStorage.GetAll;
using Application.CQRS.Categories.Queries.CategoryStorage.GetById;
using Application.CQRS.Posts.Models;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Constants;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : MyBaseController
    {
        #region CategoryStorage

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetListOfCategoriesAsync()
        {
            IEnumerable<CategoryDto> categories = await Mediator.Send(new GetListOfCategoriesQuery());
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("paged-list")]
        public async Task<ActionResult<IPagedList<CategoryDto>>> GetPagedListOfCategoriesAsync(
            [FromQuery] GetPagedListOfCategoriesQuery request)
        {
            IPagedList<CategoryDto> categories = await Mediator.Send(request);
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryAsync([FromRoute] Guid id)
        {
            CategoryDto categoryDto = await Mediator.Send(new GetCategoryByIdQuery {CategoryId = id});
            return Ok(categoryDto);
        }
        
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<Guid>> PostCategoryAsync([FromBody] CreateCategoryCommand request)
        {
            Guid createdCategoryId = await Mediator.Send(request);
            return Ok(createdCategoryId);
        }

        [Authorize(Roles = Roles.Admin)]
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

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCategoryCommand {CategoryId = id});
            return NoContent();
        }

        #endregion

        #region CategoryPost

        [AllowAnonymous]
        [HttpGet("{categoryId}/posts")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsOfCategoryAsync([FromRoute] Guid categoryId)
        {
            IEnumerable<PostDto> posts =
                await Mediator.Send(new GetListOfCategoryPostsQuery { CategoryId = categoryId });

            return Ok(posts);
        }

        #endregion
    }
}