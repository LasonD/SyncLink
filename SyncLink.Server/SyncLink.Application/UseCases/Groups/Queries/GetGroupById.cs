using AutoMapper;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Dtos;
using SyncLink.Application.UseCases.Queries.GetById.Base;

namespace SyncLink.Application.UseCases.Groups.Queries;

public class GetGroupById
{
    public record Query : GetById.Query<Group, GroupDto>;

    public class Handler : GetById.Handler<Group, GroupDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;

        public Handler(IEntityRepository<Group> genericRepository, IMapper mapper, IUserRepository userRepository, IGroupsRepository groupsRepository) : base(genericRepository, mapper)
        {
            _userRepository = userRepository;
            _groupsRepository = groupsRepository;
        }

        public override async Task<GroupDto> Handle(GetById.Query<Group, GroupDto> request, CancellationToken cancellationToken)
        {
            var dto = await base.Handle(request, cancellationToken);

            var isAdmin = await _userRepository.IsUserAdminOfGroupAsync(request.UserId!.Value, dto.Id, cancellationToken);

            dto.IsAdmin = isAdmin;
            dto.MembersCount = await _groupsRepository.GetGroupMembersCountAsync(dto.Id, cancellationToken);

            return dto;
        }

        // protected override Task<bool> CheckUserHasAccessAsync(Base.GetById.Query<Domain.Group, GroupDto> request, CancellationToken cancellationToken)
        // {
        //     if (request.UserId == null)
        //     {
        //         return false;
        //     }
        //
        //     var user = _userRepository.
        // }
    }
}