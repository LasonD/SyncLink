using MediatR;
using SyncLink.Application.Contracts.Dtos;

namespace SyncLink.Application.UseCases.Authenticate;

public class AuthenticateRequest : IRequest<AuthResult>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}