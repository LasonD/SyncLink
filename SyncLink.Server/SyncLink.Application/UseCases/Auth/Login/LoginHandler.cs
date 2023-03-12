﻿using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Auth.Login;

public class LoginHandler : IRequestHandler<LoginRequest, AuthResult>
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;

    public LoginHandler(IAuthRepository authRepository, IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }

    public async Task<AuthResult> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            return await HandleInternalAsync(request, cancellationToken);
        }
        catch (RepositoryActionException ex)
        {
            throw new LoginException(ex.GetClientFacingErrors());
        }
    }

    private async Task<AuthResult> HandleInternalAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var loginData = _mapper.Map<LoginData>(request);

        var result = await _authRepository.AuthenticateUserAsync(loginData, cancellationToken);

        var authResult = result.GetResult();

        return authResult;
    }
}