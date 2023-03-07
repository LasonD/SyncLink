using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Dtos;

namespace SyncLink.Application.UseCases.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthResult>
{
    private readonly IAuthRepository _authRepository;

    public AuthenticateHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<AuthResult> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        // TODO: use AutoMapper
        var loginData = new LoginData()
        {
            Password = request.Password,
            UsernameOrEmail = request.UsernameOrEmail,
        };

        var result = await _authRepository.AuthenticateUserAsync(loginData, cancellationToken);

        // TODO: rewrite it in a better way
        if (result.Status != RepositoryActionStatus.Ok)
        {
            throw new Exception($"{string.Join(", ", result.Errors!)})");
        }

        var authResult = result.Result!;

        return authResult;
    }
}