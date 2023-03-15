using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;
using SyncLink.Common.Helpers;

namespace SyncLink.Application.UseCases.Commands.Auth.Register;

public partial class Register
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

        public async Task<AuthResult> Handle(Command command, CancellationToken cancellationToken)
        {
            try
            {
                return await HandleInternalAsync(command, cancellationToken);
            }
            catch (RepositoryActionException ex)
            {
                throw new RegistrationException(ex.GetClientFacingErrors());
            }
        }

        private async Task<AuthResult> HandleInternalAsync(Command command, CancellationToken cancellationToken)
        {
            var registerData = MapRegisterData(command);

            var result = await _authRepository.RegisterUserAsync(registerData, cancellationToken);

            var authResult = result.GetResult();

            return authResult;
        }

        private RegistrationData MapRegisterData(Command command)
        {
            var registerData = _mapper.Map<RegistrationData>(command);

            if (registerData.UserName.IsNullOrWhiteSpace())
            {
                registerData.UserName = $"{registerData.FirstName}_{registerData.LastName}";
            }

            return registerData;
        }
    }
}

