using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Auth.Login;

public class LoginRequest : IRequest<AuthResult>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}