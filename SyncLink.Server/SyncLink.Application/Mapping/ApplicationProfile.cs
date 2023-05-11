using AutoMapper;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;
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

        CreateMap<Group, GroupOverviewDto>();
        CreateMap<UserGroup, DomainUserDto>()
            .ForAllMembers(opt => opt.MapFrom(src => src.User));
        CreateMap<UserGroup, GroupDto>()
            .ForAllMembers(opt => opt.MapFrom(src => src.Group));

        CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.UserGroups));
        CreateMap<Room, RoomDto>();
        CreateMap<User, DomainUserDto>();

        CreateMap(typeof(PaginatedEnumerable<>), typeof(PaginatedEnumerable<>));
    }
}