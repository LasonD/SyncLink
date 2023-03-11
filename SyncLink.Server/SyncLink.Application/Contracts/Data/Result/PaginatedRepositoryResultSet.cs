using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Contracts.Data.Result;

public class PaginatedRepositoryResultSet<TEntity> : RepositoryEntityResult<IPaginatedEnumerable<TEntity>>
{
    public PaginatedRepositoryResultSet(RepositoryActionStatus status, IPaginatedEnumerable<TEntity>? result, Exception? exception = null, IEnumerable<RepositoryError>? errors = null) : base(status, result, exception, errors)
    {
    }
}