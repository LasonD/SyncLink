using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Exceptions;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class GroupsRepository : GenericEntityRepository<Group>, IGroupRepository
{
    public GroupsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RepositoryEntityResult<Group>> CreateGroupForUserAsync(int userId, Group group, CancellationToken cancellationToken)
    {
        var userResult = await _userRepository.GetByIdAsync(request.UserId, cancellationToken,
            include: u => u.UserGroups);

        var user = userResult.GetResult();

        if (user.UserGroups.Any(ug => ug.Group.Name == request.Name))
        {
            throw new BusinessException($"User {user.UserName} already has group with name {request.Name}");
        }

        var group = new Group(request.Name, request.Description);

        var userGroup = new UserGroup(user, group, isCreator: true, isAdmin: true);

        _groupRepository.CreateAsync();
    }
}