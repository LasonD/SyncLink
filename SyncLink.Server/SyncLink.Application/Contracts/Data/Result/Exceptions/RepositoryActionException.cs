using SyncLink.Common.Helpers.Extensions;

namespace SyncLink.Application.Contracts.Data.Result.Exceptions;

public class RepositoryActionException : Exception
{
    public RepositoryActionException(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType, Exception? innerException = null) : 
        base(GetFormattedErrorMessage(status, errors, entityType), innerException)
    {
        Status = status;
        EntityType = entityType;
        Errors = errors?.ToList().AsReadOnly();
    }

    public RepositoryActionStatus Status { get; }

    public Type EntityType { get; }

    public IReadOnlyCollection<RepositoryError>? Errors { get; }

    public ICollection<string> GetClientFacingErrors()
    {
        return GetClientFacingErrors(Status, Errors?.ToList(), EntityType);
    }

    private static string GetFormattedErrorMessage(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType)
    {
        return string.Join(", ", GetClientFacingErrors(status, errors, entityType));
    }

    private static ICollection<string> GetClientFacingErrors(RepositoryActionStatus status, ICollection<RepositoryError>? errors, Type entityType)
    {
        if (errors.IsNullOrEmpty())
        {
            return new List<string> { $"Unexpected result for '{entityType.Name}': {status}" };
        }

        return errors!.Where(e => e.IsClientFacing).Select(e => e.ToString()).ToList();
    }
}