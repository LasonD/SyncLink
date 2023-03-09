using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Auth.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, AuthResult>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public RegisterHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<AuthResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var registerData = _mapper.Map<RegistrationData>(request);

            var result = await _authRepository.RegisterUserAsync(registerData, cancellationToken);

            // TODO: rewrite it in a better way
            if (result.Status != RepositoryActionStatus.Ok)
            {
                throw new RegistrationException(result.Errors?.Select(e => e.ToString()));
            }

            var authResult = result.Result!;

            return authResult;
        }
    }
}