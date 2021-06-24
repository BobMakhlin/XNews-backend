using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Roles.Commands.RoleStorage;
using Application.CQRS.Roles.Models;
using Application.CQRS.Roles.Queries.RoleStorage;
using Application.CQRS.Roles.Queries.RoleUser;
using Application.CQRS.Users.Models;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Constants;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : MyBaseController
    {
        #region RoleStorage

        [HttpGet]
        public async Task<ActionResult<IPagedList<RoleDto>>> GetAllRolesAsync([FromQuery] GetPagedListOfRolesQuery request)
        {
            IPagedList<RoleDto> roles = await Mediator.Send(request);
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleByIdAsync([FromRoute] string id)
        {
            RoleDto role = await Mediator.Send(new GetRoleByIdQuery {RoleId = id});
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostRoleAsync([FromBody] CreateRoleCommand request)
        {
            string roleId = await Mediator.Send(request);
            return Ok(roleId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleAsync([FromRoute] string id, [FromBody] UpdateRoleCommand request)
        {
            if (id != request.RoleId)
            {
                return BadRequest();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync([FromRoute] string id)
        {
            await Mediator.Send(new DeleteRoleCommand {RoleId = id});
            return NoContent();
        }

        #endregion

        #region RoleUser

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersOfRoleAsync([FromRoute] string id)
        {
            IEnumerable<UserDto> usersOfRole = await Mediator.Send(new GetListOfRoleUsersQuery {RoleId = id});
            return Ok(usersOfRole);
        }

        #endregion
    }
}