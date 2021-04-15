using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            PostDto post = await Mediator.Send(new GetPostByIdQuery() {PostId = id});

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
    }
}