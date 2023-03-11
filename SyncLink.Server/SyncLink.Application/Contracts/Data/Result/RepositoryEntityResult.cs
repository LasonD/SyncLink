using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Common.Helpers;
using System.Diagnostics;

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

    public TEntity GetResult()
    {
        if (Status == RepositoryActionStatus.Ok)
        {
            return Result!;
        }

        var errorMessage = GetFormattedErrorMessage();

        throw Status switch
        {
            RepositoryActionStatus.NotFound => new NotFoundException(errorMessage, Exception),
            RepositoryActionStatus.Conflict => new ConflictException(errorMessage, Exception),
            RepositoryActionStatus.UnknownError => new RepositoryActionException(errorMessage, Exception),
            _ => new UnreachableException("Wtf ??", Exception)
        };
    }

    public static RepositoryEntityResult<TEntity> NotFound() => new(RepositoryActionStatus.NotFound, null);

    public static RepositoryEntityResult<TEntity> Updated(TEntity result) => new(RepositoryActionStatus.Updated, result);

    public static RepositoryEntityResult<TEntity> Deleted(TEntity result) => new(RepositoryActionStatus.Deleted, result);

    public static RepositoryEntityResult<TEntity> Ok(TEntity result) => new(RepositoryActionStatus.Ok, result);

    public static RepositoryEntityResult<TEntity> Conflict(IEnumerable<RepositoryError>? errors) => new(RepositoryActionStatus.Conflict, null, errors: errors?.ToList());

    private string GetFormattedErrorMessage()
    {
        if (Errors.IsNullOrEmpty())
        {
            return $"Unexpected repository action state for '{typeof(TEntity).Name}': {Status}";
        }

        return string.Join(", ", Errors!) ;
    }
}