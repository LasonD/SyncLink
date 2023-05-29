using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.Auth.Login;

public partial class Login
{
    public record Command : IRequest<AuthResult>
    {
        public string UsernameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}