using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Rooms.Queries;

public static class GetRoomMembers
{
    public class Query : IRequest<IPaginatedResult<RoomMemberDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int RoomId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<RoomMemberDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public async Task<IPaginatedResult<RoomMemberDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var messagesResult = await _usersRepository.GetRoomMembersAsync(
                request.GroupId,
                request.RoomId,
                new OrderedPaginationQuery<UserRoom>(request.PageNumber, request.PageSize),
                cancellationToken
            );

            var messages = messagesResult.GetResult();
            var dto = _mapper.Map<PaginatedResult<RoomMemberDto>>(messages);

            return dto;
        }
    }
}