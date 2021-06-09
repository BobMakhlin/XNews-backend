using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Models;
using Application.CQRS.PostRates.Models;
using Application.CQRS.Posts.Models;
using Application.CQRS.Roles.Models;
using Application.CQRS.Users.Commands.UserPassword;
using Application.CQRS.Users.Commands.UserRole;
using Application.CQRS.Users.Commands.UserStorage;
using Application.CQRS.Users.Models;
using Application.CQRS.Users.Queries;
using Application.CQRS.Users.Queries.UserComment;
using Application.CQRS.Users.Queries.UserCommentRate;
using Application.CQRS.Users.Queries.UserPost;
using Application.CQRS.Users.Queries.UserPostRate;
using Application.CQRS.Users.Queries.UserRole;
using Application.CQRS.Users.Queries.UserStorage;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Controllers.Abstraction;
using Presentation.API.Requests.Common;
using Presentation.API.Requests.ControllerRequests;

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

        [HttpGet("{id}/posts")]
        public async Task<IActionResult> GetPostsOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<PostDto> posts = await Mediator.Send(new GetPostsOfUserQuery
            {
                UserId = id, 
                PageNumber = paginationRequest.PageNumber, 
                PageSize = paginationRequest.PageSize
            });
            return Ok(posts);
        }
        
        [HttpGet("{id}/postRates")]
        public async Task<IActionResult> GetPostRatesOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<PostRateDto> postRates = await Mediator.Send(new GetPostRatesOfUserQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(postRates);
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<CommentDto> comments = await Mediator.Send(new GetCommentsOfUserQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(comments);
        }
        
        [HttpGet("{id}/commentRates")]
        public async Task<IActionResult> GetCommentRatesOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<CommentRateDto> commentRates = await Mediator.Send(new GetCommentRatesOfUserQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(commentRates);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] string id)
        {
            UserDto user = await Mediator.Send(new GetUserByIdQuery {UserId = id});
            return Ok(user);
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

        [HttpPost("{id}/change-password")]
        public async Task<IActionResult> ChangeUserPasswordAsync([FromRoute] string id,
            [FromBody] UsersControllerRequests.ChangeUserPasswordRequest request)
        {
            await Mediator.Send(new ChangeUserPasswordCommand
            {
                UserId = id,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            });
            return NoContent();
        }
    }
}