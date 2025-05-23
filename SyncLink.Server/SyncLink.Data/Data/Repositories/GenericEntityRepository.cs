﻿using System.Linq.Expressions;
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

    public Task<RepositoryEntityResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] inclusions)
    {
        return GetByIdAsync<TEntity>(id, cancellationToken, inclusions);
    }

    public async Task<RepositoryEntityResult<TLocalEntity>> GetByIdAsync<TLocalEntity>(int id, CancellationToken cancellationToken = default, params Expression<Func<TLocalEntity, object>>[] inclusions) where TLocalEntity : EntityBase 
    {
        cancellationToken.ThrowIfCancellationRequested();

        var query = DbContext.Set<TLocalEntity>().AsQueryable<TLocalEntity>();

        foreach (var inclusion in inclusions)
        {
            query = query.Include(inclusion);
        }

        var entity = await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (entity == null)
        {
            return RepositoryEntityResult<TLocalEntity>.NotFound();
        }

        return RepositoryEntityResult<TLocalEntity>.Ok(entity);
    }

    public virtual async Task<PaginatedRepositoryResultSet<TLocalEntity>> GetBySpecificationAsync<TLocalEntity>(OrderedPaginationQuery<TLocalEntity> specification, CancellationToken cancellationToken) where TLocalEntity : class
    {
        var (query, totalCount) = await GetQueryWithAppliedSpecificationAsync(specification, cancellationToken);
        var items = await query.ToListAsync(cancellationToken);

        return items.ToPaginatedOkResult(specification.Page, specification.PageSize, totalCount);
    }

    protected async Task<(IQueryable<TLocalEntity> query, int totalCount)> GetQueryWithAppliedSpecificationAsync<TLocalEntity>(OrderedPaginationQuery<TLocalEntity> specification, CancellationToken cancellationToken) where TLocalEntity : class
    {
        var set = DbContext.Set<TLocalEntity>();
        var queryWithCount = await ApplyQuerySpecificationAsync<TLocalEntity>(set, specification, cancellationToken);
        return queryWithCount;
    }

    public virtual Task<PaginatedRepositoryResultSet<TEntity>> GetBySpecificationAsync(OrderedPaginationQuery<TEntity> specification, CancellationToken cancellationToken)
    {
        return GetBySpecificationAsync<TEntity>(specification, cancellationToken);
    }

    protected async Task<(IQueryable<TEntity> query, int totalCount)> ApplyQuerySpecificationAsync(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData, CancellationToken cancellationToken)
    {
        return await ApplyQuerySpecificationAsync<TEntity>(query, queryData, cancellationToken);
    }

    protected async Task<(IQueryable<TLocalEntity> query, int totalCount)> ApplyQuerySpecificationAsync<TLocalEntity>(IQueryable<TLocalEntity> query, OrderedPaginationQuery<TLocalEntity> queryData, CancellationToken cancellationToken) where TLocalEntity : class
    {
        query = ApplyFiltering(query, queryData);
        query = ApplyOrdering(query, queryData);
        query = ApplyInclusions(query, queryData);
        query = ApplySearching(query, queryData);
        var totalCount = await query.CountAsync(cancellationToken);
        query = ApplyPagination(query, queryData);
        
        return (query, totalCount);
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
        return ApplyPagination<TEntity>(query, queryData);
    }

    private static IQueryable<TLocalEntity> ApplyPagination<TLocalEntity>(IQueryable<TLocalEntity> query, OrderedPaginationQuery<TLocalEntity> queryData)
    {
        var itemsToSkipCount = (queryData.Page - 1) * queryData.PageSize;
        var itemsToTakeCount = queryData.PageSize;

        query = query.Skip(itemsToSkipCount).Take(itemsToTakeCount);
        return query;
    }

    private static IQueryable<TEntity> ApplySearching(IQueryable<TEntity> query, OrderedPaginationQuery<TEntity> queryData)
    {
        return ApplySearching<TEntity>(query, queryData);
    }

    private static IQueryable<TLocalEntity> ApplySearching<TLocalEntity>(IQueryable<TLocalEntity> query, OrderedPaginationQuery<TLocalEntity> queryData)
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
        return ApplyInclusions<TEntity>(query, queryData);
    }

    private static IQueryable<TLocalEntity> ApplyInclusions<TLocalEntity>(IQueryable<TLocalEntity> query, OrderedPaginationQuery<TLocalEntity> queryData) where TLocalEntity : class
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
        return ApplyOrdering<TEntity>(query, queryData);
    }

    private static IQueryable<TLocalEntity> ApplyOrdering<TLocalEntity>(IQueryable<TLocalEntity> query, OrderedPaginationQuery<TLocalEntity> queryData)
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
        return ApplyFiltering<TEntity>(query, queryData);
    }

    private static IQueryable<TLocalEntity> ApplyFiltering<TLocalEntity>(IQueryable<TLocalEntity> query, OrderedPaginationQuery<TLocalEntity> queryData)
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