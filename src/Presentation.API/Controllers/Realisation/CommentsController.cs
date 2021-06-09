using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Commands.CommentRate;
using Application.CQRS.Comments.Commands.CommentStorage;
using Application.CQRS.Comments.Queries.CommentRate;
using Presentation.API.Controllers.Abstraction;
using Presentation.API.Requests.ControllerRequests;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : MyBaseController
    {
        #region CommentRate

        [HttpGet("{id}/rates")]
        public async Task<IActionResult> GetRatesOfCommentAsync([FromRoute] Guid id)
        {
            IEnumerable<CommentRateDto> rates = await Mediator.Send(new GetRatesOfCommentQuery {CommentId = id});
            return Ok(rates);
        }
        
        [HttpPost("{id}/rates")]
        public async Task<IActionResult> AddRateToCommentAsync([FromRoute] Guid id,
            [FromBody] CommentsControllerRequests.AddRateToCommentRequest request)
        {
            await Mediator.Send(new AddRateToCommentCommand
            {
                CommentId = id,
                Rate = request.Rate,
                UserId = request.UserId
            });
            return NoContent();
        }

        #endregion

        #region CommentStorage

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentCommand request)
        {
            Guid createdCommentId = await Mediator.Send(request);
            return Ok(createdCommentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommentAsync([FromRoute] Guid id,
            [FromBody] UpdateCommentCommand request)
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

        #endregion
    }
}