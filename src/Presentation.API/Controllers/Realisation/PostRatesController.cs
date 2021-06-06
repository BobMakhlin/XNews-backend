using System;
using System.Threading.Tasks;
using Application.CQRS.PostRates.Commands;
using Application.CQRS.PostRates.Queries;
using Application.CQRS.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class PostRatesController : MyBaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreatePostRateAsync([FromBody] CreatePostRateCommand request)
        {
            Guid createdPostRateId = await Mediator.Send(request);
            return Ok(createdPostRateId);
        }
        
        [HttpGet("{id}/author")]
        public async Task<IActionResult> GetAuthorOfPostRate([FromRoute] Guid id)
        {
            UserDto user = await Mediator.Send(new GetAuthorOfPostRateQuery {PostRateId = id});
            return Ok(user);
        }
    }
}