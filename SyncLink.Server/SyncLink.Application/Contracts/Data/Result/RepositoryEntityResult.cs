using SyncLink.Data.Models;

namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryEntityResult<TEntity> : RepositoryResult where TEntity : EntityBase
{
    public RepositoryEntityResult(RepositoryActionStatus status, TEntity? result, Exception? exception = null) : base(status, exception)
    {
        Result = result;
    }

    public TEntity? Result { get; }

    public static RepositoryEntityResult<TEntity> NotFound() => new(RepositoryActionStatus.NotFound, null);

    public static RepositoryEntityResult<TEntity> Updated(TEntity result) => new(RepositoryActionStatus.Updated, result);

    public static RepositoryEntityResult<TEntity> Deleted(TEntity result) => new(RepositoryActionStatus.Deleted, result);

    public static RepositoryEntityResult<TEntity> Ok(TEntity result) => new(RepositoryActionStatus.Ok, result);
}