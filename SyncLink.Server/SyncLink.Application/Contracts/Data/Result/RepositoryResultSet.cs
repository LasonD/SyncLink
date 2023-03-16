namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryResultSet<TEntity> : RepositoryEntityResult<IEnumerable<TEntity>>
{
    public RepositoryResultSet(RepositoryActionStatus status, IEnumerable<TEntity>? result, Exception? exception = null, ICollection<RepositoryError>? errors = null) : base(status, result, exception, errors)
    {
    }

    public static RepositoryResultSet<TEntity> OkSet(IEnumerable<TEntity> result) => new(RepositoryActionStatus.Ok, result);
}