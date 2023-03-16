using System.Linq.Expressions;

namespace SyncLink.Application.Contracts.Data;

public class OrderedPaginationQuery<TEntity>
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;

    public int Page { get; set; } = DefaultPage;

    public int PageSize { get; set; } = DefaultPageSize;

    public IEnumerable<Expression<Func<TEntity, object>>> OrderingExpressions { get; set; } = new List<Expression<Func<TEntity, object>>>();

    public IEnumerable<Expression<Func<TEntity, bool>>> FilteringExpressions { get; set; } = new List<Expression<Func<TEntity, bool>>>();
}