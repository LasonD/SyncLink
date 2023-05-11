using MediatR;
using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.UseCases.Queries.GetById.Base;

public abstract partial class GetById
{
    public abstract record Query<TEntity, TDto> : IRequest<TDto> where TEntity : EntityBase
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
    }
}