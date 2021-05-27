using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Roles.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id)
        {
            UserDto user = await Mediator.Send(new GetUserByIdQuery {UserId = id});
            return Ok(user);
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRolesOfUserAsync([FromRoute] string id)
        {
            IEnumerable<RoleDto> userRoles = await Mediator.Send(new GetRolesOfUserQuery {UserId = id});
            return Ok(userRoles);
        }
    }
}