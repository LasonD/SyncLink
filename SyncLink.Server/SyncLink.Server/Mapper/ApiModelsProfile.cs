using AutoMapper;
using SyncLink.Application.UseCases.Commands.Auth.Login;
using SyncLink.Application.UseCases.Commands.Auth.Register;
using SyncLink.Application.UseCases.Commands.CreateGroup;
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