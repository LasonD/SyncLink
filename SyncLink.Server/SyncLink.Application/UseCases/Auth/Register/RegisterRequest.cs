using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Auth.Register;

public partial class Register
{
    public class Command : IRequest<AuthResult>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
    }
}