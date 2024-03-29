﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Categories.Models;
using Application.CQRS.Comments.Models;
using Application.CQRS.PostRates.Models;
using Application.CQRS.Posts.Commands.PostCategory;
using Application.CQRS.Posts.Commands.PostRate;
using Application.CQRS.Posts.Commands.PostStorage;
using Application.CQRS.Posts.Models;
using Application.CQRS.Posts.Queries.PostCategory;
using Application.CQRS.Posts.Queries.PostComment;
using Application.CQRS.Posts.Queries.PostRate;
using Application.CQRS.Posts.Queries.PostStorage;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Requests.Common;
using Presentation.API.Requests.ControllerRequests;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : MyBaseController
    {
        #region PostStorage

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IPagedList<PostDto>>> GetAllPostsAsync([FromQuery] GetPagedListOfPostsQuery request)
        {
            IPagedList<PostDto> posts = await Mediator.Send(request);
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostAsync([FromRoute] Guid id)
        {
            PostDto post = await Mediator.Send(new GetPostByIdQuery {PostId = id});
            return Ok(post);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePostAsync([FromBody] CreatePostCommand request)
        {
            Guid createdPostId = await Mediator.Send(request);
            return Ok(createdPostId);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeletePostCommand {PostId = id});
            return NoContent();
        }

        #endregion

        #region PostCategory

        [AllowAnonymous]
        [HttpGet("{postId}/categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesOfPostAsync([FromRoute] Guid postId)
        {
            IEnumerable<CategoryDto> categories =
                await Mediator.Send(new GetListOfPostCategoriesQuery {PostId = postId});
            return Ok(categories);
        }

        [Authorize]
        [HttpPost("{postId}/categories/{categoryId}")]
        public async Task<IActionResult> AddCategoryToPostAsync([FromRoute] Guid postId, [FromRoute] Guid categoryId)
        {
            await Mediator.Send(new AddCategoryToPostCommand {PostId = postId, CategoryId = categoryId});
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{postId}/categories/{categoryId}")]
        public async Task<IActionResult> DeleteCategoryOfPostAsync([FromRoute] Guid postId, [FromRoute] Guid categoryId)
        {
            await Mediator.Send(new DeleteCategoryOfPostCommand {PostId = postId, CategoryId = categoryId});
            return NoContent();
        }

        #endregion

        #region PostRate

        [AllowAnonymous]
        [HttpGet("{postId}/rates")]
        public async Task<ActionResult<IEnumerable<PostRateDto>>> GetRatesOfPostAsync([FromRoute] Guid postId)
        {
            IEnumerable<PostRateDto> rates = await Mediator.Send(new GetListOfPostRatesQuery {PostId = postId});
            return Ok(rates);
        }

        [Authorize]
        [HttpPost("{id}/rates")]
        public async Task<IActionResult> AddRateToPostAsync([FromRoute] Guid id,
            [FromBody] PostsControllerRequests.AddRateToPostRequest request)
        {
            await Mediator.Send(new AddRateToPostCommand
            {
                PostId = id,
                UserId = request.UserId,
                Rate = request.Rate
            });
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{postId}/rates/{userId}")]
        public async Task<IActionResult> RemoveRateOfPostAsync([FromRoute] Guid postId, [FromRoute] string userId)
        {
            await Mediator.Send(new RemoveRateOfPostCommand {PostId = postId, UserId = userId});
            return NoContent();
        }

        #endregion

        #region PostComment

        [AllowAnonymous]
        [HttpGet("{id}/comments")]
        public async Task<ActionResult<IPagedList<CommentDto>>> GetCommentsOfPostAsync([FromRoute] Guid id,
            [FromQuery] PaginationRequest request)
        {
            IPagedList<CommentDto> comments = await Mediator.Send(new GetPagedListOfPostCommentsQuery
            {
                PostId = id,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });
            return Ok(comments);
        }

        #endregion
    }
}