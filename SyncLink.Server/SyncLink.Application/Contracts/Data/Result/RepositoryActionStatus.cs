namespace SyncLink.Application.Contracts.Data.Result;

public enum RepositoryActionStatus
{
    Created = 0,
    Ok = 1,
    Updated = 2,
    Deleted = 3,
    NotFound = 4,
    Conflict = 5,
    UnknownError = 6,
}