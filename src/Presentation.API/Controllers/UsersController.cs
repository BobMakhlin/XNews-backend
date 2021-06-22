using System.Collections.Generic;
using System.Threading.Tasks;
using Application.CQRS.CommentRates.Models;
using Application.CQRS.Comments.Models;
using Application.CQRS.PostRates.Models;
using Application.CQRS.Posts.Models;
using Application.CQRS.Roles.Models;
using Application.CQRS.Users.Commands.UserAuthentication;
using Application.CQRS.Users.Commands.UserPassword;
using Application.CQRS.Users.Commands.UserRole;
using Application.CQRS.Users.Commands.UserStorage;
using Application.CQRS.Users.Models;
using Application.CQRS.Users.Queries.UserComment;
using Application.CQRS.Users.Queries.UserCommentRate;
using Application.CQRS.Users.Queries.UserPost;
using Application.CQRS.Users.Queries.UserPostRate;
using Application.CQRS.Users.Queries.UserRole;
using Application.CQRS.Users.Queries.UserStorage;
using Application.Identity.Models;
using Application.Pagination.Common.Models.PagedList;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Requests.Common;
using Presentation.API.Requests.ControllerRequests;
using Presentation.Common.ControllerAbstractions;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : MyBaseController
    {
        #region UserStorage

        [HttpGet]
        public async Task<ActionResult<IPagedList<UserDto>>> GetAllUsersAsync(
            [FromQuery] GetPagedListOfUsersQuery request)
        {
            IPagedList<UserDto> users = await Mediator.Send(request);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync([FromRoute] string id)
        {
            UserDto user = await Mediator.Send(new GetUserByIdQuery {UserId = id});
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> PostUserAsync([FromBody] CreateUserCommand request)
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

        #endregion

        #region UserPost

        [HttpGet("{id}/posts")]
        public async Task<ActionResult<IPagedList<PostDto>>> GetPostsOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<PostDto> posts = await Mediator.Send(new GetPagedListOfPostsMadeByUserQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(posts);
        }

        #endregion

        #region UserPostRate

        [HttpGet("{id}/postRates")]
        public async Task<ActionResult<IPagedList<PostRateDto>>> GetPostRatesOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<PostRateDto> postRates = await Mediator.Send(new GetPagedListOfPostRatesMadeByUserQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(postRates);
        }

        #endregion

        #region UserComment

        [HttpGet("{id}/comments")]
        public async Task<ActionResult<IPagedList<CommentDto>>> GetCommentsOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<CommentDto> comments = await Mediator.Send(new GetPagedListOfUserCommentsQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(comments);
        }

        #endregion

        #region UserCommentRate

        [HttpGet("{id}/commentRates")]
        public async Task<ActionResult<IPagedList<CommentRateDto>>> GetCommentRatesOfUserAsync([FromRoute] string id,
            [FromQuery] PaginationRequest paginationRequest)
        {
            IPagedList<CommentRateDto> commentRates = await Mediator.Send(new GetPagedListOfCommentRatesMadeByUserQuery
            {
                UserId = id,
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize
            });
            return Ok(commentRates);
        }

        #endregion

        #region UserRole

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRolesOfUserAsync([FromRoute] string id)
        {
            IEnumerable<RoleDto> rolesOfUser = await Mediator.Send(new GetListOfUserRolesQuery {UserId = id});
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

        #endregion

        #region UserPassword

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

        #endregion

        #region UserAuthentication

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticateCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("refresh-session")]
        public async Task<ActionResult<AuthenticationResponse>> RefreshSession([FromBody] RefreshSessionCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand request)
        {
            await Mediator.Send(request);
            return NoContent();
        }

        #endregion
    }
}