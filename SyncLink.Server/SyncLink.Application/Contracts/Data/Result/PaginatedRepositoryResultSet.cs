using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Contracts.Data.Result;

public class PaginatedRepositoryResultSet<TEntity> : RepositoryEntityResult<IPaginatedEnumerable<TEntity>>
{
    public PaginatedRepositoryResultSet(RepositoryActionStatus status, IPaginatedEnumerable<TEntity>? result, Exception? exception = null, ICollection<RepositoryError>? errors = null) : base(status, result, exception, errors)
    {
    }

    public static PaginatedRepositoryResultSet<TEntity> OkSet(IPaginatedEnumerable<TEntity> result) => new(RepositoryActionStatus.Ok, result);
}