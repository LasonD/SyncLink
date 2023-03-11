namespace SyncLink.Application.Contracts.Data.Result.Exceptions;

internal class ConflictException : RepositoryActionException
{
    public ConflictException(string message, Exception? innerException) : base(message, innerException)
    {

    }
}