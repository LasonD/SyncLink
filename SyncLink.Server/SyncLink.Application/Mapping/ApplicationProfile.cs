using System.Collections;
using AutoMapper;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Application.Dtos;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Dtos.WordsChainGame;
using SyncLink.Application.UseCases.Auth.Commands.Login;
using SyncLink.Application.UseCases.Auth.Commands.Register;

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
        CreateMap<UserRoom, RoomMemberDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
        CreateMap<UserWordsChainGame, WordsChainGameParticipantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.IsCreator, opt => opt.MapFrom(src => src.IsCreator))
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));

        CreateMap<Group, GroupDto>();
        CreateMap<Room, RoomDto>();
        CreateMap<User, GroupMemberDto>();
        CreateMap<Message, MessageDto>();
        CreateMap<Whiteboard, WhiteboardDto>();
        CreateMap<WhiteboardElementDto, WhiteboardElement>().ReverseMap();
        CreateMap<WhiteboardElementOptionsDto, WhiteboardElementOptions>().ReverseMap();
        CreateMap<WordsChainGame, WordsChainGameDto>()
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants))
            .ReverseMap();
        CreateMap<WordsChainGame, WordsChainGameOverviewDto>().ReverseMap();
        CreateMap<WordsChainEntry, WordsChainGameEntryDto>().ReverseMap();

        CreateMap<TextPlotGame, TextPlotGameDto>().ReverseMap();
        CreateMap<TextPlotGame, TextPlotGameWithEntriesDto>().ReverseMap();
        CreateMap<TextPlotEntry, TextPlotEntryDto>().ReverseMap();
        CreateMap<TextPlotVote, TextPlotVoteDto>().ReverseMap();

        CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
        CreateMap(typeof(PaginatedResult<>), typeof(List<>)).ConvertUsing(typeof(PaginatedResultToListConverter<,>));
    }

    public class PaginatedResultToListConverter<T, TDto> : ITypeConverter<PaginatedResult<T>, List<TDto>>
    {
        private readonly IMapper _mapper;

        public PaginatedResultToListConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<TDto> Convert(PaginatedResult<T> source, List<TDto> destination, ResolutionContext context)
        {
            return source.Entities.Select(e => _mapper.Map<TDto>(e)).ToList();
        }
    }
}