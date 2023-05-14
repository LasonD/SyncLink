using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Domain;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Queries;

public static class GetMessages
{
    public class Query : IRequest<IPaginatedResult<MessageDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int? RoomId { get; init; }
        public int? OtherUserId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<MessageDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IRoomsRepository _roomsRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository, IMessagesRepository messagesRepository, IRoomsRepository roomsRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _messagesRepository = messagesRepository;
            _roomsRepository = roomsRepository;
        }

        public async Task<IPaginatedResult<MessageDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            if (request.RoomId != null)
            {
                return await RetrieveRoomMessagesAsync(request.GroupId, request.RoomId.Value, request.PageNumber, request.PageSize, cancellationToken);
            }

            if (request.OtherUserId != null)
            {
                return await RetrievePrivateMessagesAsync(request, cancellationToken);
            }

            throw new BusinessException("Messages source should be provided.");
        }

        private async Task<IPaginatedResult<MessageDto>> RetrievePrivateMessagesAsync(Query request, CancellationToken cancellationToken)
        {
            var privateRoomResult = await _roomsRepository.GetPrivateRoomAsync(request.GroupId, request.UserId, request.OtherUserId!.Value, cancellationToken);

            var privateRoom = privateRoomResult.GetResult();

            return await RetrieveRoomMessagesAsync(request.GroupId, privateRoom.Id, request.PageNumber, request.PageSize, cancellationToken).ConfigureAwait(false);
        }

        private async Task<IPaginatedResult<MessageDto>> RetrieveRoomMessagesAsync(int groupId, int roomId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var messagesResult = await _messagesRepository.GetRoomMessagesAsync(
                groupId,
                roomId,
                new OrderedPaginationQuery<Message>(pageNumber, pageSize),
                cancellationToken
            );

            var messages = messagesResult.GetResult();
            var dto = _mapper.Map<PaginatedResult<MessageDto>>(messages);

            return dto;
        }
    }
}