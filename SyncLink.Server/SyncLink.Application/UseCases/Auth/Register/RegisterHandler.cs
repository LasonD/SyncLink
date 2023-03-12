using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
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
            try
            {
                return await HandleInternalAsync(request, cancellationToken);
            }
            catch (RepositoryActionException ex)
            {
                throw new RegistrationException(ex.Errors.AsEnumerable())
            }
        }

        private async Task<AuthResult> HandleInternalAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var registerData = _mapper.Map<RegistrationData>(request);

            var result = await _authRepository.RegisterUserAsync(registerData, cancellationToken);

            var authResult = result.GetResult();

            return authResult;
        }
    }
}