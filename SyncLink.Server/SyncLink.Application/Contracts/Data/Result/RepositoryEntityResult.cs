namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryEntityResult<TEntity> : RepositoryResult where TEntity : class
{
    public RepositoryEntityResult(
        RepositoryActionStatus status,
        TEntity? result,
        Exception? exception = null,
        IEnumerable<RepositoryError>? errors = null
    ) : base(status, exception, errors)
    {
        Result = result;
    }

    public TEntity? Result { get; }

    public static RepositoryEntityResult<TEntity> NotFound() => new(RepositoryActionStatus.NotFound, null);

    public static RepositoryEntityResult<TEntity> Updated(TEntity result) => new(RepositoryActionStatus.Updated, result);

    public static RepositoryEntityResult<TEntity> Deleted(TEntity result) => new(RepositoryActionStatus.Deleted, result);

    public static RepositoryEntityResult<TEntity> Ok(TEntity result) => new(RepositoryActionStatus.Ok, result);

    public static RepositoryEntityResult<TEntity> Conflict(IEnumerable<RepositoryError>? errors) => new(RepositoryActionStatus.Conflict, null, errors: errors?.ToList());
}