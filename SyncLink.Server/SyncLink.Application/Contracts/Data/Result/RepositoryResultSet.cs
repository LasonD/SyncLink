using SyncLink.Data.Models;

namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryResultSet<TEntity> : RepositoryResult where TEntity : EntityBase
{
    public RepositoryResultSet(RepositoryActionStatus status, IEnumerable<TEntity> results, Exception? exception = null) : base(status, exception)
    {
        Results = results;
    }

    public virtual IEnumerable<TEntity> Results { get; }

    public static RepositoryResultSet<TEntity> Ok(IEnumerable<TEntity> results) => new(RepositoryActionStatus.Ok, results.ToList());
}