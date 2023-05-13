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

        CreateMap<UserGroup, GroupMemberDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.IsCreator, opt => opt.MapFrom(src => src.IsCreator))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        CreateMap<UserGroup, GroupDto>()
            .ForAllMembers(opt => opt.MapFrom(src => src.Group));
        CreateMap<Group, GroupDto>();
        CreateMap<Room, RoomDto>();
        CreateMap<User, GroupMemberDto>();
        CreateMap<Message, MessageDto>();

        CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
    }
}