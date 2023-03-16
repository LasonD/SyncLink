using System.Linq.Expressions;

namespace SyncLink.Application.Contracts.Data;

public class OrderedPaginationQuery<TEntity>
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;

    public int Page { get; set; } = DefaultPage;

    public int PageSize { get; set; } = DefaultPageSize;

    public IEnumerable<OrderingCriteria<TEntity>> OrderingExpressions { get; set; } = new List<OrderingCriteria<TEntity>>();

    public IEnumerable<Expression<Func<TEntity, bool>>> FilteringExpressions { get; set; } = new List<Expression<Func<TEntity, bool>>>();

    public IEnumerable<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; } = new List<Expression<Func<TEntity, object>>>();

    public IEnumerable<SearchingCriteria<TEntity>> SearchTerms { get; set; } = new List<SearchingCriteria<TEntity>>();
}

public record SearchingCriteria<TEntity>(Expression<Func<TEntity, string>> Expression, ICollection<string> Terms);

public record OrderingCriteria<TEntity>(Expression<Func<TEntity, object>> Expression, bool IsAscending = true)
{
    public static explicit operator Expression<Func<TEntity, object>>(OrderingCriteria<TEntity> orderingCriteria)
    {
        return orderingCriteria.Expression;
    }

    public static explicit operator OrderingCriteria<TEntity>(Expression<Func<TEntity, object>> expression)
    {
        return new OrderingCriteria<TEntity>(expression);
    }
}