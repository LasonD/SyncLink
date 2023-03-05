using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    [HttpPost("/login")]
    public Task<IActionResult> Login([FromBody] LoginDto loginData)
    {
        return Task.FromResult<IActionResult>(Ok(loginData));
    }

    [HttpPost("/register")]
    public Task<IActionResult> Register([FromBody] RegistrationDto registerData)
    {
        return Task.FromResult<IActionResult>(Ok(registerData));
    }
}