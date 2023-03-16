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
        private IGroupRepository _groupRepository;

        public Handler(IRoomsRepository roomsRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _roomsRepository = roomsRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public Task<RoomDto> Handle(Command request, CancellationToken cancellationToken)
        {
            
        }
    }
}