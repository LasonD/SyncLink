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
        public int RoomId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<MessageDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly IMessagesRepository _messagesRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository, IMessagesRepository messagesRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _messagesRepository = messagesRepository;
        }

        public async Task<IPaginatedResult<MessageDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var messagesResult = await _messagesRepository.GetRoomMessagesAsync(
                request.GroupId,
                request.RoomId,
                new OrderedPaginationQuery<Message>(request.PageNumber, request.PageSize),
                cancellationToken
            );

            var messages = messagesResult.GetResult();
            var dto = _mapper.Map<PaginatedResult<MessageDto>>(messages);

            return dto;
        }
    }
}