using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Commands;
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
        
        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync(CreateCommentCommand request)
        {
            Guid createdCommentId = await Mediator.Send(request);
            return Ok(createdCommentId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommentAsync(Guid id, UpdateCommentCommand request)
        {
            if (id != request.CommentId)
            {
                return BadRequest();
            }
            
            await Mediator.Send(request);

            return NoContent();
        }
    }
}