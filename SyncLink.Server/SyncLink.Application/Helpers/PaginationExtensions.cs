using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Helpers;

public static class PaginationExtensions
{
    public static IPaginatedEnumerable<TResult> ToPaginated<TSource, TResult>(this IPaginatedEnumerable<TSource> source, IEnumerable<TResult> resultItems)
    {
        return new PaginatedEnumerable<TResult>(resultItems, source.ItemCount, source.Page, source.PageSize);
    }
}