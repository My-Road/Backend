using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Common;
using MyRoad.API.Users.RequestDto;
using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;

namespace MyRoad.API.Users
{
    [Route("api/v{version:apiVersion}/UserProfile")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserProfileController(IUserService userService,
     IUserContext userContext) : ControllerBase
    {
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserProfileDto dto)
        {
            var userId = userContext.Id;
            var response = await userService.UpdateAsync(userId, dto.ToDomainUser());
            return ResponseHandler.HandleResult(response);
        }

        [Authorize]
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            var userId = userContext.Id;
            var userResult = await userService.GetByIdAsync(userId);
            return ResponseHandler.HandleResult(userResult);
        }
    }
}
