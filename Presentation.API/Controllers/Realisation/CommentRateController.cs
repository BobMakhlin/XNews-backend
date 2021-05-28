using System;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Commands;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.CommentRates.Queries;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CommentRateController : MyBaseController
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

        [HttpGet("of/user")]
        public async Task<IActionResult> GetCommentRatesOfUserAsync([FromQuery] GetCommentRatesOfUserQuery request)
        {
            IPagedList<CommentRateDto> userCommentRates = await Mediator.Send(request);
            return Ok(userCommentRates);
        }
    }
}