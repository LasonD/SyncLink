using System.Linq.Expressions;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data
{
    public abstract class Specification<TEntity, TQuery>
        where TEntity : EntityBase 
        where TQuery : OrderedPaginationQuery
    {
        protected static Dictionary<string, Expression<Func<TEntity, object>>> OrderingLookup = new(StringComparer.InvariantCultureIgnoreCase);

        protected static Expression<Func<TEntity, object>> DefaultOrdering = _ => 1;

        protected Specification(TQuery query)
        {
            Query = query;
        }

        public TQuery Query { get; }

        public abstract Expression<Func<TEntity, bool>> GetFilteringCondition();

        public abstract IEnumerable<IEnumerable<(Expression<Func<TEntity, string>> prop, string[] terms)>> GetSearchTerms();

        public virtual IEnumerable<Expression<Func<TEntity, object>>> GetInclusions()
        {
            yield break;
        }

        public Expression<Func<TEntity, object>> GetOrderByExpression()
        {
            if (Query.OrderBy == null)
            {
                return DefaultOrdering;
            }

            OrderingLookup.TryGetValue(Query.OrderBy, out var orderBy);

            return orderBy ?? DefaultOrdering;
        }

        public Expression<Func<TEntity, object>> GetThenByExpression()
        {
            if (Query.ThenBy == null)
            {
                return DefaultOrdering;
            }

            OrderingLookup.TryGetValue(Query.ThenBy, out var thenBy);

            return thenBy ?? DefaultOrdering;
        }
    }
}
