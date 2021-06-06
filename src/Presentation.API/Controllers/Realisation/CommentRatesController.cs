using System;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Commands;
using Application.CQRS.CommentRates.Queries;
using Application.CQRS.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CommentRatesController : MyBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCommentRate([FromBody] CreateCommentRateCommand request)
        {
            Guid createdCommentRateId = await Mediator.Send(request);
            return Ok(createdCommentRateId);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentRateAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCommentRateCommand {CommentRateId = id});
            return NoContent();
        }

        [HttpGet("{id}/author")]
        public async Task<IActionResult> GetAuthorOfCommentRate([FromRoute] Guid id)
        {
            UserDto user = await Mediator.Send(new GetAuthorOfCommentRateQuery {CommentRateId = id});
            return Ok(user);
        }

    }
}