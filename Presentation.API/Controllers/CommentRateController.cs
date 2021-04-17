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
    }
}