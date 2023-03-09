using AutoMapper;
using SyncLink.Application.Dtos;
using SyncLink.Application.UseCases.Auth.Login;
using SyncLink.Application.UseCases.Auth.Register;

namespace SyncLink.Application.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<LoginRequest, LoginData>();
        CreateMap<RegisterRequest, RegistrationData>();
    }
}