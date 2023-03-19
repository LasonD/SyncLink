using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.CreateRoom;

public partial class CreateRoom
{
    public record Handler : IRequestHandler<Command, RoomDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly IRoomsRepository _roomsRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository, IRoomsRepository roomsRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _roomsRepository = roomsRepository;
        }

        public async Task<RoomDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var usersResult = await _usersRepository.GetUsersFromGroupAsync(request.GroupId, request.UserIds, cancellationToken);

            var users = usersResult.GetResult();

            var room = new Room(request.Name);

            room.AddMembers(users);

            await _roomsRepository.CreateAsync(room, cancellationToken);

            await _roomsRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoomDto>(room);
        }
    }
}