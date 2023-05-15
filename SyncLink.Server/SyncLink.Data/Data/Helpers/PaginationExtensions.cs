using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Infrastructure.Data.Helpers;

public static class PaginationExtensions
{
    public static IPaginatedResult<T> ToPaginatedResult<T>(this ICollection<T> items, int page, int pageSize, int totalCount)
    {
        var paginatedEnumerable = new PaginatedResult<T>(items, totalCount, page, pageSize);
        return paginatedEnumerable;
    }

    public static PaginatedRepositoryResultSet<T> ToPaginatedOkResult<T>(this ICollection<T> items, int page, int pageSize, int totalCount)
    {
        var paginatedResult = items.ToPaginatedResult(page, pageSize, totalCount);
        return PaginatedRepositoryResultSet<T>.OkSet(paginatedResult);
    }
}