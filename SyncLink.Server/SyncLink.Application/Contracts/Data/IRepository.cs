using System.Linq.Expressions;
using Application.Application.Queries;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Data.Models;

namespace SyncLink.Application.Contracts.Data;

internal interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<RepositoryEntityResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] inclusions);

    Task<PaginatedRepositoryResultSet<TEntity>> GetBySpecificationAsync<TQuery>(Specification<TEntity, TQuery> specification, CancellationToken cancellationToken) where TQuery : OrderedPaginationQuery;

    Task<RepositoryEntityResult<TEntity>> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken);

    Task<RepositoryEntityResult<TEntity>> DeleteAsync(long id, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}