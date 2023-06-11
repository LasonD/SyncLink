using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Groups.Queries;

public static class GetGroupJoinRequests
{
    public record Query(int GroupId, int UserId) : IRequest<IPaginatedResult<GroupJoinRequestDto>>;

    public class Handler : IRequestHandler<Query, IPaginatedResult<GroupJoinRequestDto>>
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

        public async Task<IPaginatedResult<GroupJoinRequestDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserGroupAdmin = await _userRepository.IsUserAdminOfGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserGroupAdmin)
            {
                throw new AuthException(new[] { "User is not an admin of the group" });
            }

            var group = (await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken,
                include: x => x.JoinRequests
            )).GetResult();

            var pendingRequests = group.JoinRequests.Where(r => r.Status == GroupJoinRequestStatus.Pending).ToList();

            return _mapper.Map<PaginatedResult<GroupJoinRequestDto>>(pendingRequests);
        }
    }
}