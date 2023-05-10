using System.Collections;

namespace SyncLink.Application.Contracts.Data.Result.Pagination;

public class PaginatedEnumerable<T> : IPaginatedEnumerable<T>
{
    protected PaginatedEnumerable()
    {

    }

    public PaginatedEnumerable(IEnumerable<T> entities, int itemCount, int page, int pageSize)
    {
        Entities = entities;
        ItemCount = itemCount;
        Page = page;
        PageSize = pageSize;
        PageCount = pageSize > 0 ? (int)Math.Ceiling(itemCount / (double)pageSize) : 0;
        LastPage = PageCount;
    }

    public IEnumerable<T> Entities { get; protected set; } = new List<T>();
    public int Page { get; protected set; }
    public int? NextPage => HasNextPage ? Page + 1 : null;
    public int? PreviousPage => HasPreviousPage ? Page - 1 : null;
    public int LastPage { get; protected set; }
    public int ItemCount { get; protected set; }
    public int PageSize { get; protected set; }
    public int PageCount { get; protected set; }
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < LastPage;

    public IEnumerator<T> GetEnumerator() => Entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}