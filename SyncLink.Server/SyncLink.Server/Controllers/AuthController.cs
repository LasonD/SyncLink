using AutoMapper;
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
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginData, CancellationToken cancellationToken)
    {
        var loginRequest = _mapper.Map<LoginRequest>(loginData);

        var authResult = await _mediator.Send(loginRequest, cancellationToken);

        return Ok(authResult);
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registerData, CancellationToken cancellationToken)
    {
        var registerRequest = _mapper.Map<RegisterRequest>(registerData);

        var authResult = await _mediator.Send(registerRequest, cancellationToken);

        return Ok(authResult);
    }
}