using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.Contracts.Data;

public interface IAuthRepository
{
    Task<RepositoryEntityResult<AuthResult>> AuthenticateUserAsync(LoginData loginData, CancellationToken cancellationToken);

    Task<RepositoryEntityResult<AuthResult>> RegisterUserAsync(RegistrationData registrationData, CancellationToken cancellationToken);
}