using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Infrastructure.Data.Helpers;

public static class PaginationExtensions
{
    public static IPaginatedEnumerable<T> ToPaginatedEnumerable<T>(this ICollection<T> items, int page, int pageSize)
    {
        var paginatedEnumerable = new PaginatedEnumerable<T>(items, items.Count, page, pageSize);
        return paginatedEnumerable;
    }

    public static PaginatedRepositoryResultSet<T> ToPaginatedOkResult<T>(this ICollection<T> items, int page, int pageSize)
    {
        var paginatedResult = items.ToPaginatedEnumerable(page, pageSize);
        return PaginatedRepositoryResultSet<T>.OkSet(paginatedResult);
    }
}