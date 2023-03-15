using AutoMapper;
using SyncLink.Application.Domain;
using SyncLink.Application.Dtos;
using SyncLink.Application.UseCases.Commands.Auth.Login;
using SyncLink.Application.UseCases.Commands.Auth.Register;

namespace SyncLink.Application.Mapping;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<Login.Command, LoginData>();
        CreateMap<Register.Command, RegistrationData>();

        CreateMap<Group, GroupDto>();
    }
}