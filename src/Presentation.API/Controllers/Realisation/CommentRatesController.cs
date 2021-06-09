using System;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Commands;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class CommentRatesController : MyBaseController
    {
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentRateAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteCommentRateCommand {CommentRateId = id});
            return NoContent();
        }
    }
}