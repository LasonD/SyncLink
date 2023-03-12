using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Auth.Login;
using SyncLink.Application.UseCases.Auth.Register;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var loginCommand = _mapper.Map<Login.Command>(loginData);

        var authResult = await _mediator.Send(loginCommand, cancellationToken);

        return Ok(authResult);
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registerData, CancellationToken cancellationToken)
    {
        var registerCommand = _mapper.Map<Register.Command>(registerData);

        var authResult = await _mediator.Send(registerCommand, cancellationToken);

        return Ok(authResult);
    }
}