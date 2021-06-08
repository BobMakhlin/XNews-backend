using System;
using System.Threading.Tasks;
using Application.CQRS.PostRates.Models;
using Application.CQRS.PostRates.Queries;
using Application.CQRS.Users.Models;
using Application.CQRS.Users.Queries;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class PostRatesController : MyBaseController
    {
        [HttpGet("{id}/author")]
        public async Task<IActionResult> GetAuthorOfPostRate([FromRoute] Guid id)
        {
            UserDto user = await Mediator.Send(new GetAuthorOfPostRateQuery {PostRateId = id});
            return Ok(user);
        }

        [HttpGet("of/user")]
        public async Task<IActionResult> GetPostRatesOfUserAsync([FromQuery] GetPostRatesOfUserQuery request)
        {
            IPagedList<PostRateDto> postRates = await Mediator.Send(request);
            return Ok(postRates);
        }
    }
}