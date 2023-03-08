using MediatR;
using SyncLink.Application.Contracts.Dtos;

namespace SyncLink.Application.UseCases.Login;

public class LoginRequest : IRequest<AuthResult>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}