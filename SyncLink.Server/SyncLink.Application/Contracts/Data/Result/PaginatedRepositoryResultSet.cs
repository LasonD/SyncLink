using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Data.Models;

namespace SyncLink.Application.Contracts.Data.Result;

internal class PaginatedRepositoryResultSet<TEntity> : RepositoryResultSet<TEntity> where TEntity : EntityBase
{
    public PaginatedRepositoryResultSet(RepositoryActionStatus status, IPaginatedEnumerable<TEntity> results, Exception? exception = null) : base(status, results, exception)
    {
        PaginatedResults = results;
    }

    public IPaginatedEnumerable<TEntity> PaginatedResults { get; }

    public override IEnumerable<TEntity> Results => PaginatedResults;
}