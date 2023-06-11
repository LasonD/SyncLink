using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Groups.Queries;

public static class GetGroupJoinRequests
{
    public record Query(int GroupId, int UserId) : IRequest<PaginatedResult<GroupJoinRequestDto>>;

    public class Handler : IRequestHandler<Query, PaginatedResult<GroupJoinRequestDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IGroupsRepository groupsRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _groupsRepository = groupsRepository;
            _userRepository = userRepository;
        }

        public async Task<PaginatedResult<GroupJoinRequestDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserGroupAdmin = await _userRepository.IsUserAdminOfGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserGroupAdmin)
            {
                throw new AuthException(new[] { "User is not an admin of the group" });
            }

            var group = (await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken,
                include: x => x.JoinRequests
            )).GetResult();

            var pendingRequests = group.JoinRequests.ToList();

            var dtoList = _mapper.Map<List<GroupJoinRequestDto>>(pendingRequests);

            return new PaginatedResult<GroupJoinRequestDto>(dtoList, dtoList.Count, 1, dtoList.Count);
        }
    }
}