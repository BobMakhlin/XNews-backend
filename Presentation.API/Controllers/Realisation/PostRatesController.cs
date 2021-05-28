using System;
using System.Threading.Tasks;
using Application.CQRS.PostRates.Commands;
using Application.CQRS.PostRates.Models;
using Application.CQRS.PostRates.Queries;
using Application.Pagination.Common.Models.PagedList;
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
        
        [HttpGet("of/user")]
        public async Task<IActionResult> GetPostRatesOfUserAsync([FromQuery] GetPostRatesOfUserQuery request)
        {
            IPagedList<PostRateDto> userPostRates = await Mediator.Send(request);
            return Ok(userPostRates);
        }
    }
}