using System.Linq.Expressions;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IEntityRepository<TEntity> where TEntity : EntityBase
{
    Task<RepositoryEntityResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] include);
    Task<RepositoryEntityResult<TLocalEntity>> GetByIdAsync<TLocalEntity>(int id, CancellationToken cancellationToken = default, params Expression<Func<TLocalEntity, object>>[] include) where TLocalEntity : EntityBase;

    Task<PaginatedRepositoryResultSet<TEntity>> GetBySpecificationAsync(OrderedPaginationQuery<TEntity> specification, CancellationToken cancellationToken);
    Task<PaginatedRepositoryResultSet<TLocalEntity>> GetBySpecificationAsync<TLocalEntity>(OrderedPaginationQuery<TLocalEntity> specification, CancellationToken cancellationToken) where TLocalEntity : class;

    Task<RepositoryEntityResult<TEntity>> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken);

    Task<RepositoryEntityResult<TEntity>> DeleteAsync(int id, CancellationToken cancellationToken);

    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}