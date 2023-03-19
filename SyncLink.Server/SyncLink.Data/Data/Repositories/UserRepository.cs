using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Helpers;

namespace SyncLink.Infrastructure.Data.Repositories;

public class UserRepository : GenericEntityRepository<User>, IUserRepository
{
    public UserRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PaginatedRepositoryResultSet<User>> GetUsersFromGroupAsync(int groupId, IEnumerable<int> userIds, CancellationToken cancellationToken)
    {
        var specification = new OrderedPaginationQuery<User>
        {
            FilteringExpressions = new List<Expression<Func<User, bool>>>()
            {
                u => userIds.Contains(u.Id),
                u => u.UserGroups.Any(ug => ug.GroupId == groupId),
            },
            Page = 0,
            PageSize = int.MaxValue 
        };

        var query = DbContext.ApplicationUsers;

        var users = await ApplyQuerySpecification(query, specification)
            .ToListAsync(cancellationToken);

        return users.ToPaginatedOkResult(specification.Page, specification.PageSize);
    }

    public Task<bool> UserHasGroupWithNameAsync(int userId, string groupName, CancellationToken cancellationToken)
    {
        return DbContext.UsersToGroups.AnyAsync(ug =>
                ug.UserId == userId &&
                ug.Group.Name == groupName &&
                ug.IsCreator, cancellationToken
        );
    }

    // private class UsersFromGroupQuery : OrderedPaginationQuery
    // {
    //     public int GroupId { get; set; }
    //     public IEnumerable<int> UserIds { get; set; }
    // }
    //
    // private class UsersFromGroupSpecification : Specification<User, UsersFromGroupQuery>
    // {
    //     public UsersFromGroupSpecification(UsersFromGroupQuery query) : base(query)
    //     {
    //     }
    //
    //     public override Expression<Func<User, bool>> GetFilteringCondition()
    //     {
    //         var groupId = Query.GroupId;
    //
    //         return u => u.UserGroups.Any(g => g.GroupId == groupId);
    //     }
    //
    //     public override IEnumerable<IEnumerable<(Expression<Func<User, string>> prop, string[] terms)>> GetSearchTerms()
    //     {
    //         return Enumerable.Empty<IEnumerable<(Expression<Func<User, string>> prop, string[] terms)>>();
    //     }
    // }
}