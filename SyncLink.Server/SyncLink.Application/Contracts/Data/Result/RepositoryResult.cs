namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryResult
{
    public RepositoryResult(RepositoryActionStatus status, Exception? exception = null, IEnumerable<RepositoryError>? errors = null)
    {
        Status = status;
        Exception = exception;
        Errors = errors;
    }

    public Exception? Exception { get; }

    public IEnumerable<RepositoryError>? Errors { get; }

    public RepositoryActionStatus Status { get; }
}