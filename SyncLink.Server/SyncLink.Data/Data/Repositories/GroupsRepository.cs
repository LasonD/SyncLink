using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Enums;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;
using System.Linq.Expressions;

namespace SyncLink.Infrastructure.Data.Repositories;

public class GroupsRepository : GenericEntityRepository<Group>, IGroupsRepository
{
    public GroupsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public Task<PaginatedRepositoryResultSet<Group>> SearchByNameAndDescriptionAsync(int userId, string[] terms, GroupSearchMode searchMode, CancellationToken cancellationToken)
    {
        var specification = new OrderedPaginationQuery<Group>()
        {
            FilteringExpressions = new List<Expression<Func<Group, bool>>>()
            {
                g => !g.IsPrivate
            },
            SearchTerms = new List<SearchingCriteria<Group>>()
            {
                new(g => g.Name, terms),
                new(g => g.Description ?? string.Empty, terms),
            }
        };

        Expression<Func<Group, bool>> filteringCondition = searchMode switch
        {
            GroupSearchMode.ExplorePublic => g => !g.IsPrivate && g.UserGroups.All(ug => ug.UserId != userId),
            GroupSearchMode.Membership => g => g.UserGroups.Any(ug => ug.UserId == userId),
            GroupSearchMode.Owned => g => g.UserGroups.Any(ug => ug.IsCreator && ug.UserId == userId),
            _ => throw new ArgumentOutOfRangeException(nameof(searchMode), searchMode, null)
        };

        specification.FilteringExpressions.Add(filteringCondition);

        return GetBySpecificationAsync<Group>(specification, cancellationToken);
    }
}