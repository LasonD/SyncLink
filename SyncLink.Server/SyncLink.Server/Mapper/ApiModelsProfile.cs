using AutoMapper;
using SyncLink.Application.UseCases.Auth.Login;
using SyncLink.Application.UseCases.Auth.Register;
using SyncLink.Application.UseCases.CreateGroup;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Mapper;

public class ApiModelsProfile : Profile
{
    public ApiModelsProfile()
    {
        CreateMap<LoginDto, Login.Command>();
        CreateMap<RegistrationDto, Register.Command>();

        CreateMap<CreateGroupDto, CreateGroup.Command>();
    }
}