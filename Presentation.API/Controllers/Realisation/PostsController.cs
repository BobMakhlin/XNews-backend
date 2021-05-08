using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Models;
using Application.CQRS.Comments.Models;
using Application.CQRS.PostRates.Models;
using Application.CQRS.Posts.Commands;
using Application.CQRS.Posts.Models;
using Application.CQRS.Posts.Queries;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync([FromQuery] GetAllPostsQuery request)
        {
            try
            {
                IPagedList<PostDto> posts = await Mediator.Send(request);
                return Ok(posts);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostAsync([FromRoute] Guid id)
        {
            PostDto post = await Mediator.Send(new GetPostByIdQuery {PostId = id});

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostCommand request)
        {
            Guid createdPostId = await Mediator.Send(request);
            return Ok(createdPostId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostAsync([FromRoute] Guid id, [FromBody] UpdatePostCommand request)
        {
            if (id != request.PostId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] Guid id)
        {
            try
            {
                await Mediator.Send(new DeletePostCommand {PostId = id});
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{postId}/categories")]
        public async Task<IActionResult> GetCategoriesOfPostAsync([FromRoute] Guid postId)
        {
            IEnumerable<CategoryDto> categories =
                await Mediator.Send(new GetAllCategoriesOfPostQuery {PostId = postId});
            return Ok(categories);
        }

        [HttpPost("{postId}/categories/{categoryId}")]
        public async Task<IActionResult> AddCategoryToPostAsync([FromRoute] Guid postId, [FromRoute] Guid categoryId)
        {
            await Mediator.Send(new AddCategoryToPostCommand {PostId = postId, CategoryId = categoryId});
            return NoContent();
        }

        [HttpDelete("{postId}/categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryOfPostAsync([FromRoute] Guid postId, [FromRoute] Guid categoryId)
        {
            await Mediator.Send(new DeleteCategoryOfPostCommand {PostId = postId, CategoryId = categoryId});
            return NoContent();
        }

        [HttpGet("{postId}/rates")]
        public async Task<IActionResult> GetRatesOfPostAsync([FromRoute] Guid postId)
        {
            IEnumerable<PostRateDto> rates = await Mediator.Send(new GetAllRatesOfPostQuery {PostId = postId});
            return Ok(rates);
        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetCommentsOfPostAsync([FromRoute] Guid postId)
        {
            try
            {
                var query = new GetAllCommentsOfPostQuery
                {
                    PostId = postId
                };
                IEnumerable<CommentDto> comments = await Mediator.Send(query);
                return Ok(comments);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}