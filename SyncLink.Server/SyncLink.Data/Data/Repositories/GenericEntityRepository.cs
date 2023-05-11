using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.Result;
using NinjaNye.SearchExtensions;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Helpers;
using SyncLink.Application.Domain.Base;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Common.Helpers.Extensions;

namespace SyncLink.Infrastructure.Data.Repositories;

public class GenericEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : EntityBase
{
    protected readonly SyncLinkDbContext DbContext;

    public GenericEntityRepository(SyncLinkDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<RepositoryEntityResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] inclusions)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = DbContext.Set<TEntity>().AsQueryable<TEntity>();

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

    public virtual async Task<PaginatedRepositoryResultSet<TEntity>> GetBySpecificationAsync(OrderedPaginationQuery<TEntity> specification, CancellationToken cancellationToken)
    {
        var set = DbContext.Set<TEntity>();
        var query = ApplyQuerySpecification(set, specification);
        var items = await query.ToListAsync(cancellationToken);

        return items.ToPaginatedOkResult(specification.Page, specification.PageSize);
    }

    protected IQueryable<TEntity> ApplyQuerySpecification(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        query = ApplyFiltering(query, queryData);
        query = ApplyOrdering(query, queryData);
        query = ApplyInclusions(query, queryData);
        query = ApplySearching(query, queryData);
        query = ApplyPagination(query, queryData);

        return query;
    }

    public async Task<RepositoryEntityResult<TEntity>> UpdateAsync(int id, TEntity entity, CancellationToken cancellationToken)
    {
        var result = await GetByIdAsync(id, cancellationToken);

        if (result.Status != RepositoryActionStatus.Ok)
        {
            return result;
        }

        var entry = result.Result!;

        DbContext.Update(entry);

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

        DbContext.Remove(entry);

        return RepositoryEntityResult<TEntity>.Deleted(entry);
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var entry = await DbContext.AddAsync(entity, cancellationToken);
        return entry.Entity;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }

    #region Queryable transformation methods

    private static IQueryable<TEntity> ApplyPagination(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        var itemsToSkipCount = (queryData.Page - 1) * queryData.PageSize;
        var itemsToTakeCount = queryData.PageSize;

        query = query.Skip(itemsToSkipCount).Take(itemsToTakeCount);
        return query;
    }

    private static IQueryable<TEntity> ApplySearching(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        if (!queryData.SearchTerms.IsNotNullOrEmpty()) return query;

        foreach (var searchTerm in queryData.SearchTerms)
        {
            query = query.Search(searchTerm.Expression).Containing(searchTerm.Terms.ToArray());
        }

        return query;
    }

    private static IQueryable<TEntity> ApplyInclusions(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        if (!queryData.IncludeExpressions.IsNotNullOrEmpty()) return query;

        foreach (var inclusion in queryData.IncludeExpressions)
        {
            query = query.Include(inclusion);
        }

        return query;
    }

    private static IQueryable<TEntity> ApplyOrdering(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        if (!queryData.OrderingExpressions.IsNotNullOrEmpty()) return query;

        var firstOrdering = queryData.OrderingExpressions.First();

        var orderedQuery = firstOrdering.IsAscending
            ? query.OrderBy(firstOrdering.Expression)
            : query.OrderByDescending(firstOrdering.Expression);

        foreach (var ordering in queryData.OrderingExpressions.Skip(1))
        {
            orderedQuery = ordering.IsAscending
                ? orderedQuery.ThenBy(ordering.Expression)
                : orderedQuery.ThenByDescending(ordering.Expression);
        }

        query = orderedQuery;

        return query;
    }

    private static IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        if (!queryData.FilteringExpressions.IsNotNullOrEmpty()) return query;

        foreach (var expression in queryData.FilteringExpressions)
        {
            query = query.Where(expression);
        }

        return query;
    }

    #endregion
}