using SyncLink.Application.Contracts.Data.Result.Exceptions;

namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryEntityResult<TEntity> : RepositoryResult where TEntity : class
{
    public RepositoryEntityResult(
        RepositoryActionStatus status,
        TEntity? result,
        Exception? exception = null,
        ICollection<RepositoryError>? errors = null
    ) : base(status, exception, errors)
    {
        Result = result;
    }

    public TEntity? Result { get; }

    public TEntity GetResult()
    {
        if (IsSuccessStatus(Status))
        {
            return Result!;
        }

        throw new RepositoryActionException(Status, Errors?.ToList(), typeof(TEntity), Exception);
    }

    public static RepositoryEntityResult<TEntity> NotFound() => new(RepositoryActionStatus.NotFound, null);

    public static RepositoryEntityResult<TEntity> Updated(TEntity result) => new(RepositoryActionStatus.Updated, result);

    public static RepositoryEntityResult<TEntity> Deleted(TEntity result) => new(RepositoryActionStatus.Deleted, result);

    public static RepositoryEntityResult<TEntity> Ok(TEntity result) => new(RepositoryActionStatus.Ok, result);

    public static RepositoryEntityResult<TEntity> Conflict(IEnumerable<RepositoryError>? errors) => new(RepositoryActionStatus.Conflict, null, errors: errors?.ToList());

    public static RepositoryEntityResult<TEntity> ValidationFailed(IEnumerable<RepositoryError>? errors) => new(RepositoryActionStatus.ValidationFailed, null, errors: errors?.ToList());
}