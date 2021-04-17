using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : MyBaseController
    {
        [HttpGet("{id}/rates")]
        public async Task<IActionResult> GetRatesOfCommentAsync(Guid id)
        {
            IEnumerable<CommentRateDto> rates = await Mediator.Send(new GetRatesOfCommentQuery {CommentId = id});
            return Ok(rates);
        }
    }
}