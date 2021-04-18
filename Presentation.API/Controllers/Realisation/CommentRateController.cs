using System;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentRateController : MyBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCommentRate(CreateCommentRateCommand request)
        {
            Guid createdCommentRateId = await Mediator.Send(request);
            return Ok(createdCommentRateId);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentRateAsync(Guid id)
        {
            await Mediator.Send(new DeleteCommentRateCommand {CommentRateId = id});
            return NoContent();
        }
    }
}