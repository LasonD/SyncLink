using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Contracts.Data.Result;

public class PaginatedRepositoryResultSet<TEntity> : RepositoryEntityResult<IPaginatedResult<TEntity>>
{
    public PaginatedRepositoryResultSet(RepositoryActionStatus status, IPaginatedResult<TEntity>? result, Exception? exception = null, ICollection<RepositoryError>? errors = null) : base(status, result, exception, errors)
    {
    }

    public static PaginatedRepositoryResultSet<TEntity> OkSet(IPaginatedResult<TEntity> result) => new(RepositoryActionStatus.Ok, result);
}