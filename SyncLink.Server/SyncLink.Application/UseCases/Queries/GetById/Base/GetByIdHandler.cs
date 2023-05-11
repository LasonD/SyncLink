using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Base;
using System.Linq.Expressions;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Queries.GetById.Base;

public abstract partial class GetById
{
    public abstract class Handler<TEntity, TDto> : IRequestHandler<Query<TEntity, TDto>, TDto> where TEntity : EntityBase
    {
        protected readonly IMapper Mapper;
        protected readonly IEntityRepository<TEntity> GenericRepository;

        protected Handler(IEntityRepository<TEntity> genericRepository, IMapper mapper)
        {
            GenericRepository = genericRepository;
            Mapper = mapper;
        }

        public async Task<TDto> Handle(Query<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            var isOperationAllowed = await CheckUserHasAccessAsync(request, cancellationToken);

            if (!isOperationAllowed)
            {
                throw new AuthException(new[] { "The user has no access to the requested resource." });
            }

            var entity = await GetEntityAsync(request, cancellationToken);

            var dto = Mapper.Map<TDto>(entity);

            return dto;
        }

        protected virtual async Task<TEntity> GetEntityAsync(Query<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            var inclusions = GetInclusions(request);

            var result = await GenericRepository.GetByIdAsync(request.Id, cancellationToken, inclusions);

            var entity = result.GetResult();

            return entity;
        }

        protected virtual Task<bool> CheckUserHasAccessAsync(Query<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        protected virtual Expression<Func<TEntity, object>>[] GetInclusions(Query<TEntity, TDto> request)
        {
            return Array.Empty<Expression<Func<TEntity, object>>>();
        }
    }
}