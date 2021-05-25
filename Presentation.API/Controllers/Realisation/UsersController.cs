using System.Threading.Tasks;
using Application.CQRS.Users.Models;
using Application.CQRS.Users.Queries;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllUsersQuery query)
        {
            IPagedList<UserDto> users = await Mediator.Send(query);
            return Ok(users);
        }
    }
}
