using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Models;
using Application.CQRS.Comments.Models;
using Application.CQRS.PostRates.Commands;
using Application.CQRS.PostRates.Models;
using Application.CQRS.Posts.Commands;
using Application.CQRS.Posts.Models;
using Application.CQRS.Posts.Queries;
using Application.CQRS.Users.Models;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;
using Presentation.API.Requests.Common;
using Presentation.API.Requests.ControllerRequests;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync([FromQuery] GetAllPostsQuery request)
        {
            IPagedList<PostDto> posts = await Mediator.Send(request);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostAsync([FromRoute] Guid id)
        {
            PostDto post = await Mediator.Send(new GetPostByIdQuery {PostId = id});
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
            await Mediator.Send(new DeletePostCommand {PostId = id});
            return NoContent();
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

        [HttpGet("{postId}/author")]
        public async Task<IActionResult> GetAuthorOfPostAsync([FromRoute] Guid postId)
        {
            UserDto user = await Mediator.Send(new GetAuthorOfPostQuery {PostId = postId});
            return Ok(user);
        }

        [HttpPost("{id}/rates")]
        public async Task<IActionResult> AddRateToPostAsync([FromRoute] Guid id,
            [FromBody] PostsControllerRequests.AddRateToPostRequest request)
        {
            await Mediator.Send(new CreatePostRateCommand
            {
                PostId = id,
                UserId = request.UserId,
                Rate = request.Rate
            });
            return NoContent();
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsOfPostAsync([FromRoute] Guid id,
            [FromQuery] PaginationRequest request)
        {
            IPagedList<CommentDto> comments = await Mediator.Send(new GetAllCommentsOfPostQuery
            {
                PostId = id,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
            return Ok(comments);
        }
    }
}