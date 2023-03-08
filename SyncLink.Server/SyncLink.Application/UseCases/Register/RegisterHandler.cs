using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Dtos;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, AuthResult>
    {
        private readonly IAuthRepository _authRepository;

        public RegisterHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            // TODO: use AutoMapper
            var registerData = new RegistrationData()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                UserName = request.UserName
            };

            var result = await _authRepository.RegisterUserAsync(registerData, cancellationToken);

            // TODO: rewrite it in a better way
            if (result.Status != RepositoryActionStatus.Ok)
            {
                throw new Exception($"{string.Join(", ", result.Errors!)})");
            }

            var authResult = result.Result!;

            return authResult;
        }
    }
}