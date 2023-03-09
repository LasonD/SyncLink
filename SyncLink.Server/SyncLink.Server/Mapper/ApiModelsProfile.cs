using AutoMapper;
using SyncLink.Application.UseCases.Login;
using SyncLink.Application.UseCases.Register;
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