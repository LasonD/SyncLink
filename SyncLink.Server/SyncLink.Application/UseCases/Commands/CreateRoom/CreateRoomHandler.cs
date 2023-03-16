using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.CreateRoom;

public partial class CreateRoom
{
    public record Handler : IRequestHandler<Command, RoomDto>
    {
        private readonly IMapper _mapper;
        private IRoomsRepository _roomsRepository;
        private IGroupsRepository _groupsRepository;

        public Handler(IRoomsRepository roomsRepository, IMapper mapper, IGroupsRepository groupsRepository)
        {
            _roomsRepository = roomsRepository;
            _mapper = mapper;
            _groupsRepository = groupsRepository;
        }

        public Task<RoomDto> Handle(Command request, CancellationToken cancellationToken)
        {
            return Task.FromResult((RoomDto)null!);
        }
    }
}