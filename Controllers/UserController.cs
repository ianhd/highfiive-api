using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace Api.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("user/auth")]
    public async Task<IActionResult> GetJwt(string accessToken)
    {
        var jwt = await _userService.Save(accessToken);
        return Ok(new
        {
            jwt
        });
    }
}
