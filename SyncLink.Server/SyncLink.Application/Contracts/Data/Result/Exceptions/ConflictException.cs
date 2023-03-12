namespace SyncLink.Application.Contracts.Data.Result.Exceptions;

internal class ConflictException : RepositoryActionException
{
    public ConflictException(string message, Exception? innerException) : base(message, innerException)
    {

    }

    public ConflictException(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType, Exception? innerException = null) :
        base(status, errors, entityType, innerException)
    {

    }
}