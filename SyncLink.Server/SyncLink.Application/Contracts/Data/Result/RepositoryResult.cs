namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryResult
{
    protected static RepositoryActionStatus[] ErrorStatuses = new[]
    {
        RepositoryActionStatus.Conflict,
        RepositoryActionStatus.NotFound,
        RepositoryActionStatus.UnknownError,
    };

    public RepositoryResult(RepositoryActionStatus status, Exception? exception = null, IEnumerable<RepositoryError>? errors = null)
    {
        Status = status;
        Exception = exception;
        Errors = errors;
    }

    public Exception? Exception { get; }

    public IEnumerable<RepositoryError>? Errors { get; }

    public RepositoryActionStatus Status { get; }

    protected bool IsErrorStatus(RepositoryActionStatus status)
    {
        return ErrorStatuses.Contains(status);
    }

    protected bool IsSuccessStatus(RepositoryActionStatus status)
    {
        return !IsErrorStatus(status);
    }
}