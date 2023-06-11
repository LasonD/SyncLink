using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Rooms.Queries;

public static class GetRoom
{
    public class Query : IRequest<RoomDto>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int? RoomId { get; init; }
        public int? UserIdForPrivateRoom { get; init; }
    }

    public class Handler : IRequestHandler<Query, RoomDto>
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

        public async Task<RoomDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var room = await ResolveRoomAsync(request, cancellationToken);

            var dto = _mapper.Map<RoomDto>(room);

            return dto;
        }

        private async Task<Room> ResolveRoomAsync(Query request, CancellationToken cancellationToken)
        {
            Room? room = null;

            if (request.RoomId != null)
            {
                room = (await _roomsRepository.GetRoomForUserAsync(request.GroupId, request.UserId, request.RoomId.Value, cancellationToken)).GetResult();
            } 
            
            if (request.UserIdForPrivateRoom != null)
            {
                room = (await _roomsRepository.GetPrivateRoomAsync(request.GroupId, request.UserId, request.UserIdForPrivateRoom.Value, cancellationToken)).GetResult();
            }

            if (room == null)
            {
                throw new BusinessException("Unable to find a group by the provided request.");
            }

            return room;
        }
    }
}