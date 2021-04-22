using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Commands;
using Application.CQRS.Comments.Queries;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : MyBaseController
    {
        [HttpGet("{id}/rates")]
        public async Task<IActionResult> GetRatesOfCommentAsync([FromRoute] Guid id)
        {
            IEnumerable<CommentRateDto> rates = await Mediator.Send(new GetRatesOfCommentQuery {CommentId = id});
            return Ok(rates);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentCommand request)
        {
            Guid createdCommentId = await Mediator.Send(request);
            return Ok(createdCommentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] Guid id, [FromBody] UpdateCommentCommand request)
        {
            if (id != request.CommentId)
            {
                return BadRequest();
            }
            
            await Mediator.Send(request);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCommentCommand {CommentId = id});
            return NoContent();
        }
    }
}