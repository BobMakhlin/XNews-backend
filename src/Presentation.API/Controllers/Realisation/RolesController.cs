using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Roles.Models;
using Application.CQRS.Roles.Queries;
using Application.CQRS.Users.Models;
using Application.Pagination.Common.Models.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;

namespace Presentation.API.Controllers.Realisation
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : MyBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync([FromQuery] GetAllRolesQuery request)
        {
            IPagedList<RoleDto> roles = await Mediator.Send(request);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] string id)
        {
            RoleDto role = await Mediator.Send(new GetRoleByIdQuery {RoleId = id});
            return Ok(role);
        }
        
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsersOfRoleAsync([FromRoute] string id)
        {
            IEnumerable<UserDto> usersOfRole = await Mediator.Send(new GetUsersOfRoleQuery {RoleId = id});
            return Ok(usersOfRole);
        }
    }
}