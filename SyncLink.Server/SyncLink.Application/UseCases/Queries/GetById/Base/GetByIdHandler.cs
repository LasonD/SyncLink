using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.UseCases.Queries.GetById.Base;

public abstract partial class GetById
{
    public abstract class Handler<TEntity, TDto> : IRequestHandler<GetById.Query<TEntity, TDto>, TDto> where TEntity : EntityBase
    {
        private readonly IMapper _mapper;
        private readonly IEntityRepository<TEntity> _genericRepository;

        protected Handler(IEntityRepository<TEntity> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<TDto> Handle(GetById.Query<TEntity, TDto> request, CancellationToken cancellationToken)
        {
            var result = await _genericRepository.GetByIdAsync(request.Id, cancellationToken);

            var entity = result.GetResult();

            var dto = _mapper.Map<TDto>(entity);

            return dto;
        }
    }
}