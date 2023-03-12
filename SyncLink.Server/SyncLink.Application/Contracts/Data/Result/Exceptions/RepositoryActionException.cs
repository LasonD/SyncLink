using SyncLink.Common.Helpers;

namespace SyncLink.Application.Contracts.Data.Result.Exceptions;

public class RepositoryActionException : Exception
{
    public RepositoryActionException(string message, Exception? innerException = null) : base(message, innerException)
    {

    }

    public RepositoryActionException(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType, Exception? innerException = null) : 
        base(GetFormattedErrorMessage(status, errors, entityType), innerException)
    {
        Status = status;
        EntityType = entityType;
        Errors = errors?.ToList().AsReadOnly();
    }

    public RepositoryActionStatus Status { get; } = RepositoryActionStatus.UnknownError;

    public Type? EntityType { get; }

    public IReadOnlyCollection<RepositoryError>? Errors { get; }

    private static string GetFormattedErrorMessage(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType)
    {
        if (errors.IsNullOrEmpty())
        {
            return $"Unexpected result for '{entityType.Name}': {status}";
        }

        return string.Join(", ", errors!.Where(e => e.IsClientFacing));
    }
}