using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Base;

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
            var entity = await GetEntityAsync(request, cancellationToken);

            var dto = Mapper.Map<TDto>(entity);

            return dto;
        }

        protected virtual async Task<TEntity> GetEntityAsync(Query<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            var result = await GenericRepository.GetByIdAsync(request.Id, cancellationToken);

            var entity = result.GetResult();

            return entity;
        }
    }
}