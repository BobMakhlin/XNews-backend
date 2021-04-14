using System.Collections.Generic;
using System.Threading.Tasks;
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
    }
}