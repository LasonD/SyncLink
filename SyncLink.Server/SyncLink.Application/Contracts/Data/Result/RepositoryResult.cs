namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryResult
{
    public RepositoryResult(RepositoryActionStatus status, Exception? exception = null)
    {
        Status = status;
        Exception = exception;
    }

    public Exception? Exception { get; }

    public RepositoryActionStatus Status { get; }
}