namespace SyncLink.Application.Contracts.Data.Result.Exceptions;

public class RepositoryActionException : Exception
{
    public RepositoryActionException(string message, Exception? innerException = null) : base(message, innerException)
    {

    }
}