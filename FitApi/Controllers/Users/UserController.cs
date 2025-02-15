using Fit.Application.DTOs.Requests.User;
using Fit.Application.Interfaces.IServices.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fit.API.Controllers.Users;

[ApiController]
[Route("v1/users")]
public class UserController(
    IUserService _userService
    ) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
    {
        await _userService.CreateAsync(request);

        return NoContent();
    }

    [HttpPatch]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
    {
        await _userService.UpdateAsync(request);

        return NoContent();
    }

    [HttpPatch("update-password")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> ChangePassword([FromBody] UserUpdatePasswordRequest request)
    {
        await _userService.UpdatePasswordAsync(request);

        return NoContent();
    }
}
