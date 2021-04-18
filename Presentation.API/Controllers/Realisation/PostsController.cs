using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Models;
using Application.CQRS.Comments.Models;
using Application.CQRS.PostRates.Models;
using Application.CQRS.Posts.Commands;
using Application.CQRS.Posts.Models;
using Application.CQRS.Posts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            IEnumerable<PostDto> posts = await Mediator.Send(new GetAllPostsQuery());
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostAsync(Guid id)
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
        public async Task<IActionResult> UpdatePostAsync(Guid id, [FromBody] UpdatePostCommand request)
        {
            if (id != request.PostId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
            await Mediator.Send(new DeletePostCommand {PostId = id});
            return NoContent();
        }

        [HttpGet("{postId}/categories")]
        public async Task<IActionResult> GetCategoriesOfPostAsync(Guid postId)
        {
            IEnumerable<CategoryDto> categories =
                await Mediator.Send(new GetAllCategoriesOfPostQuery {PostId = postId});
            return Ok(categories);
        }

        [HttpPost("{postId}/categories/{categoryId}")]
        public async Task<IActionResult> AddCategoryToPostAsync(Guid postId, Guid categoryId)
        {
            await Mediator.Send(new AddCategoryToPostCommand {PostId = postId, CategoryId = categoryId});
            return NoContent();
        }

        [HttpDelete("{postId}/categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryOfPostAsync(Guid postId, Guid categoryId)
        {
            await Mediator.Send(new DeleteCategoryOfPostCommand {PostId = postId, CategoryId = categoryId});
            return NoContent();
        }

        [HttpGet("{postId}/rates")]
        public async Task<IActionResult> GetRatesOfPostAsync(Guid postId)
        {
            IEnumerable<PostRateDto> rates = await Mediator.Send(new GetAllRatesOfPostQuery {PostId = postId});
            return Ok(rates);
        }

        [HttpGet("{postId}/comments")]
        public async Task<IActionResult> GetCommentsOfPostAsync(Guid postId)
        {
            IEnumerable<CommentDto> comments = await Mediator.Send(new GetAllCommentsOfPostQuery {PostId = postId});
            return Ok(comments);
        }
    }
}