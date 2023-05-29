using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;
using SyncLink.Application.Domain;

namespace SyncLink.Application.UseCases.Queries;

public static class GetGroupRooms
{
    public class Query : IRequest<IPaginatedResult<RoomDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<RoomDto>>
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

        public async Task<IPaginatedResult<RoomDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var roomsResult = await _roomsRepository.GetRoomsForUserAsync(request.GroupId, request.UserId, new OrderedPaginationQuery<Room>(request.PageNumber, request.PageSize), cancellationToken);

            var groupMembers = roomsResult.GetResult();
            var dto = _mapper.Map<PaginatedResult<RoomDto>>(groupMembers);

            return dto;
        }
    }
}