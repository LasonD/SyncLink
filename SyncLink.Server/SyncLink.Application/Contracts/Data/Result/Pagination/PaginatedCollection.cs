using System.Collections;

namespace SyncLink.Application.Contracts.Data.Result.Pagination;

public class PaginatedEnumerable<T> : IPaginatedEnumerable<T>
{
    private readonly IEnumerable<T> _entities;

    public PaginatedEnumerable(IEnumerable<T> entities, int itemCount, int page, int pageSize)
    {
        _entities = entities;
        ItemCount = itemCount;
        Page = page;
        PageSize = pageSize;
        PageCount = pageSize > 0 ? (int)Math.Ceiling(itemCount / (double)pageSize) : 0;
        LastPage = PageCount;
    }

    public int Page { get; }
    public int? NextPage => HasNextPage ? Page + 1 : null;
    public int? PreviousPage => HasPreviousPage ? Page - 1 : null;
    public int LastPage { get; }
    public int ItemCount { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < LastPage;

    public IEnumerator<T> GetEnumerator() => _entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}