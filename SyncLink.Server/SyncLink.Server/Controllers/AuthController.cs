using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Login;
using SyncLink.Application.UseCases.Register;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginData, CancellationToken cancellationToken)
    {
        var authResult = await _mediator.Send(new LoginRequest
        {
            Password = loginData.Password,
            UsernameOrEmail = loginData.UsernameOrEmail
        }, cancellationToken);

        return Ok(authResult);
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registerData, CancellationToken cancellationToken)
    {
        var authResult = await _mediator.Send(new RegisterRequest
        {
            Password = registerData.Password,
            Email = registerData.Email,
            FirstName = registerData.FirstName,
            LastName = registerData.LastName,
            UserName = registerData.Username,
        }, cancellationToken);

        return Ok(authResult);
    }
}