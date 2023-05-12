using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Helpers;

public static class PaginationExtensions
{
    public static IPaginatedResult<TResult> ToPaginated<TSource, TResult>(this IPaginatedResult<TSource> source, IEnumerable<TResult> resultItems)
    {
        return new PaginatedResult<TResult>(resultItems, source.ItemCount, source.Page, source.PageSize);
    }
}