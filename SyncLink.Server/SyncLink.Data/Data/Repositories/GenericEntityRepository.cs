using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result;
using NinjaNye.SearchExtensions;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Helpers;

namespace SyncLink.Infrastructure.Data.Repositories;

public class GenericEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : EntityBase
{
    private readonly SyncLinkDbContext _dbContext;

    public GenericEntityRepository(SyncLinkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RepositoryEntityResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] inclusions)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = _dbContext.Set<TEntity>().AsQueryable<TEntity>();

        foreach (var inclusion in inclusions)
        {
            query = query.Include(inclusion);
        }

        var entity = await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity == null)
        {
            return RepositoryEntityResult<TEntity>.NotFound();
        }

        return RepositoryEntityResult<TEntity>.Ok(entity);
    }

    public virtual async Task<PaginatedRepositoryResultSet<TEntity>> GetBySpecificationAsync<TQuery>(Specification<TEntity, TQuery> specification, CancellationToken cancellationToken)
        where TQuery : OrderedPaginationQuery
    {
        var queryInfo = specification.Query;
        var query = PrepareQuery(specification);
        var items = await query.ToListAsync(cancellationToken);

        return items.ToPaginatedOkResult(queryInfo.Page, queryInfo.PageSize);
    }

    public async Task<RepositoryEntityResult<TEntity>> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken)
    {
        var result = await GetByIdAsync(id, cancellationToken);

        if (result.Status != RepositoryActionStatus.Ok)
        {
            return result;
        }

        var entry = result.Result!;

        _dbContext.Update(entry);

        return RepositoryEntityResult<TEntity>.Updated(entry);
    }

    public async Task<RepositoryEntityResult<TEntity>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await GetByIdAsync(id, cancellationToken);

        if (result.Status != RepositoryActionStatus.Ok)
        {
            return result;
        }

        var entry = result.Result!;

        _dbContext.Remove(entry);

        return RepositoryEntityResult<TEntity>.Deleted(entry);
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var entry = await _dbContext.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    #region Queryable transformation methods

    private IQueryable<TEntity> PrepareQuery<TQuery>(Specification<TEntity, TQuery> specification)
        where TQuery : OrderedPaginationQuery
    {
        var set = _dbContext.Set<TEntity>();
        var filteredQuery = ApplyFiltering(set, specification);
        var queryWithInclusions = ApplyInclusions(filteredQuery, specification);
        var paginatedQuery = ApplyPagination(queryWithInclusions, specification);
        var searchQuery = ApplySearching(paginatedQuery, specification);
        var orderedQuery = ApplyOrdering(searchQuery, specification);

        return orderedQuery.AsNoTracking();
    }

    private IQueryable<TEntity> ApplySearching<TQuery>(IQueryable<TEntity> query, Specification<TEntity, TQuery> specification)
        where TQuery : OrderedPaginationQuery
    {
        foreach (var searchTerms in specification.GetSearchTerms().Select(st => st.ToArray()))
        {
            var props = searchTerms.Select(t => t.prop).ToArray();
            var terms = searchTerms.SelectMany(t => t.terms).ToArray();
            query = query.Search(props).Containing(terms);
        }

        return query;
    }

    private IQueryable<TEntity> ApplyPagination<TQuery>(IQueryable<TEntity> query, Specification<TEntity, TQuery> specification)
        where TQuery : OrderedPaginationQuery
    {
        var queryData = specification.Query;

        var itemsToSkipCount = (queryData.Page - 1) * queryData.PageSize;
        var itemsToTakeCount = queryData.PageSize;

        return query.Skip(itemsToSkipCount).Take(itemsToTakeCount);
    }

    private IQueryable<TEntity> ApplyInclusions<TQuery>(IQueryable<TEntity> query, Specification<TEntity, TQuery> specification)
        where TQuery : OrderedPaginationQuery
    {
        foreach (var inclusion in specification.GetInclusions())
        {
            query = query.Include(inclusion);
        }

        return query;
    }

    private IQueryable<TEntity> ApplyFiltering<TQuery>(IQueryable<TEntity> query, Specification<TEntity, TQuery> specification)
        where TQuery : OrderedPaginationQuery
    {
        var filter = specification.GetFilteringCondition();
        return query.Where(filter);
    }

    private IOrderedQueryable<TEntity> ApplyOrdering<TQuery>(IQueryable<TEntity> query, Specification<TEntity, TQuery> specification)
        where TQuery : OrderedPaginationQuery
    {
        var orderBy = specification.GetOrderByExpression();
        var thenBy = specification.GetThenByExpression();

        return query.OrderBy(orderBy).ThenBy(thenBy);
    }

    #endregion
}