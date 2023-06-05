using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Rooms.Commands.CreateRoom;

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
            var completeUserIds = request.MemberIds.Append(request.UserId).Distinct().ToList();

            var usersResult = await _usersRepository.GetUsersFromGroupAsync(request.GroupId, completeUserIds, cancellationToken);

            var users = usersResult.GetResult();

            var creator = users.Entities.Single(u => u.Id == request.UserId);
            var others = users.Entities.Where(u => u != creator).ToList();

            CheckAllUsersBelongToGroup(request, users);

            var room = new Room(request.Name, request.Description, request.GroupId, creator, others);

            await _roomsRepository.CreateAsync(room, cancellationToken);

            await _roomsRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoomDto>(room);
        }

        private static void CheckAllUsersBelongToGroup(Command request, IPaginatedResult<User> users)
        {
            var userDifference = users.Entities
                .ExceptBy(request.MemberIds, u => u.Id)
                .ToList();

            if (userDifference.Count > 1)
            {
                throw new BusinessException($"Users with ids {string.Join(", ", userDifference)} don't belong to group with id {request.GroupId}");
            }
        }
    }
}