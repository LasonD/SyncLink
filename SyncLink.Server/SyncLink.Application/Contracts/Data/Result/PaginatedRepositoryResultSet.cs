using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Contracts.Data.Result;

public class PaginatedRepositoryResultSet<TEntity> : RepositoryResult
{
    public PaginatedRepositoryResultSet(RepositoryActionStatus status, IPaginatedEnumerable<TEntity> results, Exception? exception = null) : base(status, exception)
    {
        Results = results;
    }

    public IPaginatedEnumerable<TEntity> Results { get; }

    public static PaginatedRepositoryResultSet<TEntity> Ok(IPaginatedEnumerable<TEntity> results) => new(RepositoryActionStatus.Ok, results);
}