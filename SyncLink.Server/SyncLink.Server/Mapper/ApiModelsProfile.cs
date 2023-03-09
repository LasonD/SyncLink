using AutoMapper;
using SyncLink.Application.UseCases.Auth.Login;
using SyncLink.Application.UseCases.Auth.Register;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Mapper;

public class ApiModelsProfile : Profile
{
    public ApiModelsProfile()
    {
        CreateMap<LoginDto, LoginRequest>();
        CreateMap<RegistrationDto, RegisterRequest>();
    }
}