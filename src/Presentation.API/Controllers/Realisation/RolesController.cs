using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Roles.Queries;
using Application.CQRS.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : MyBaseController
    {
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsersOfRoleAsync([FromRoute] string id)
        {
            IEnumerable<UserDto> usersOfRole = await Mediator.Send(new GetUsersOfRoleQuery {RoleId = id});
            return Ok(usersOfRole);
        }
    }
}