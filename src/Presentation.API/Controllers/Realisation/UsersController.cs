using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.Roles.Models;
using Application.CQRS.Users.Commands.UserPassword;
using Application.CQRS.Users.Commands.UserRole;
using Application.CQRS.Users.Commands.UserStorage;
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
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllUsersQuery request)
        {
            IPagedList<UserDto> users = await Mediator.Send(request);
            return Ok(users);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostUserAsync([FromBody] CreateUserCommand request)
        {
            string userId = await Mediator.Send(request);
            return Ok(userId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAsync([FromRoute] string id, [FromBody] UpdateUserCommand request)
        {
            if (id != request.UserId)
            {
                return BadRequest();
            }
            
            await Mediator.Send(request);
            
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] string id)
        {
            await Mediator.Send(new DeleteUserCommand {UserId = id});
            return NoContent();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> GetRolesOfUserAsync([FromRoute] string id)
        {
            IEnumerable<RoleDto> rolesOfUser = await Mediator.Send(new GetRolesOfUserQuery {UserId = id});
            return Ok(rolesOfUser);
        }

        [HttpPost("{userId}/roles/{roleId}")]
        public async Task<IActionResult> AddRoleToUserAsync([FromRoute] string userId, [FromRoute] string roleId)
        {
            await Mediator.Send(new AddRoleToUserCommand {UserId = userId, RoleId = roleId});
            return NoContent();
        }

        [HttpDelete("{userId}/roles/{roleId}")]
        public async Task<IActionResult> DeleteRoleOfUserAsync([FromRoute] string userId, [FromRoute] string roleId)
        {
            await Mediator.Send(new DeleteRoleOfUserCommand {UserId = userId, RoleId = roleId});
            return NoContent();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }
    }
}