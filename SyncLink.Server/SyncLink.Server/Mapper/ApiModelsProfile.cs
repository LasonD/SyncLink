using AutoMapper;
using SyncLink.Application.UseCases.Auth.Commands.Login;
using SyncLink.Application.UseCases.Auth.Commands.Register;
using SyncLink.Application.UseCases.Features.Whiteboard.Commands;
using SyncLink.Application.UseCases.Groups.Commands.CreateGroup;
using SyncLink.Application.UseCases.Messages.Commands.SendMessage;
using SyncLink.Application.UseCases.Rooms.Commands.CreateRoom;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Mapper;

public class ApiModelsProfile : Profile
{
    public ApiModelsProfile()
    {
        CreateMap<LoginDto, Login.Command>();
        CreateMap<RegistrationDto, Register.Command>();

        CreateMap<CreateGroupDto, CreateGroup.Command>();
        CreateMap<CreateRoomDto, CreateRoom.Command>();
        CreateMap<CreateWhiteboardDto, CreateWhiteboard.Command>();

        CreateMap<SendMessageDto, SendMessage.Command>()
            .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId == 0 ? null : src.RoomId))
            .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientId == 0 ? null : src.RecipientId));
    }
}