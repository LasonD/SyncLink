namespace SyncLink.Application.Contracts.Data.Result.Exceptions;

public class NotFoundException : RepositoryActionException
{
    public NotFoundException(string message, Exception? innerException) : base(message, innerException)
    {

    }

    public NotFoundException(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType, Exception? innerException = null) : 
        base(status, errors, entityType, innerException)
    {

    }

    public NotFoundException(Type entityType, int id) : base($"'{entityType.Name}' not found with id '{id}'.")
    {

    }

    public static NotFoundException For<T>(int id) => new(typeof(T), id);
}