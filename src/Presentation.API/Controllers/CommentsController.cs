using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Commands.CommentRate;
using Application.CQRS.Comments.Commands.CommentStorage;
using Application.CQRS.Comments.Queries.CommentRate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Requests.ControllerRequests;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : MyBaseController
    {
        #region CommentRate

        [AllowAnonymous]
        [HttpGet("{id}/rates")]
        public async Task<ActionResult<IEnumerable<CommentRateDto>>> GetRatesOfCommentAsync([FromRoute] Guid id)
        {
            IEnumerable<CommentRateDto> rates = await Mediator.Send(new GetListOfCommentRatesQuery {CommentId = id});
            return Ok(rates);
        }
        
        [Authorize]
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

        [Authorize]
        [HttpDelete("{commentId}/rates/{userId}")]
        public async Task<IActionResult> RemoveRateOfCommentAsync([FromRoute] Guid commentId, [FromRoute] string userId)
        {
            await Mediator.Send(new RemoveRateOfCommentCommand {CommentId = commentId, UserId = userId});
            return NoContent();
        }

        #endregion

        #region CommentStorage

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCommentAsync([FromBody] CreateCommentCommand request)
        {
            Guid createdCommentId = await Mediator.Send(request);
            return Ok(createdCommentId);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCommentCommand {CommentId = id});
            return NoContent();
        }

        #endregion
    }
}