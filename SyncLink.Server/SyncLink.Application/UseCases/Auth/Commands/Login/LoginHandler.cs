using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Commands.Auth.Login;

public partial class Login
{
    public class Handler : IRequestHandler<Command, AuthResult>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public Handler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<AuthResult> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleInternalAsync(request, cancellationToken);
            }
            catch (RepositoryActionException ex)
            {
                throw new LoginException(new[] { "Invalid username, email or password" });
            }
        }

        private async Task<AuthResult> HandleInternalAsync(Command request, CancellationToken cancellationToken)
        {
            var loginData = _mapper.Map<LoginData>(request);

            var result = await _authRepository.AuthenticateUserAsync(loginData, cancellationToken);

            var authResult = result.GetResult();

            return authResult;
        }
    }
}

