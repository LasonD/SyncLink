using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Groups.Queries;

public static class GetGroupMembers
{
    public class Query : IRequest<IPaginatedResult<GroupMemberDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<GroupMemberDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public async Task<IPaginatedResult<GroupMemberDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var membersResult = await _usersRepository.GetGroupMembersAsync(request.GroupId, new OrderedPaginationQuery<UserGroup>
            {
                Page = request.PageNumber,
                PageSize = request.PageSize,
            }, cancellationToken);

            var groupMembers = membersResult.GetResult();
            var dto = _mapper.Map<PaginatedResult<GroupMemberDto>>(groupMembers);

            return dto;
        }
    }
}