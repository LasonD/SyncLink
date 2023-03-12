using SyncLink.Application.Contracts.Data.Result.Exceptions;
using System.Diagnostics;

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
        if (Status == RepositoryActionStatus.Ok)
        {
            return Result!;
        }

        throw Status switch
        {
            RepositoryActionStatus.NotFound => new NotFoundException(Status, Errors?.ToList(), typeof(TEntity), Exception),
            RepositoryActionStatus.Conflict => new ConflictException(Status, Errors?.ToList(), typeof(TEntity), Exception),
            RepositoryActionStatus.UnknownError => new RepositoryActionException(Status, Errors?.ToList(), typeof(TEntity), Exception),
            _ => new UnreachableException("Wtf ??", Exception)
        };
    }

    public static RepositoryEntityResult<TEntity> NotFound() => new(RepositoryActionStatus.NotFound, null);

    public static RepositoryEntityResult<TEntity> Updated(TEntity result) => new(RepositoryActionStatus.Updated, result);

    public static RepositoryEntityResult<TEntity> Deleted(TEntity result) => new(RepositoryActionStatus.Deleted, result);

    public static RepositoryEntityResult<TEntity> Ok(TEntity result) => new(RepositoryActionStatus.Ok, result);

    public static RepositoryEntityResult<TEntity> Conflict(IEnumerable<RepositoryError>? errors) => new(RepositoryActionStatus.Conflict, null, errors: errors?.ToList());
}