namespace SyncLink.Application.Contracts.Data.Result.Pagination;

public interface IPaginatedEnumerable<out T>
{
    IEnumerable<T> Entities { get; }
    int Page { get; }
    int? NextPage { get; }
    int? PreviousPage { get; }
    int LastPage { get; }
    int ItemCount { get; }
    int PageSize { get; }
    int PageCount { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}