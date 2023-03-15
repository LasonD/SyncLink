using MediatR;
using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.UseCases.Queries.GetById;

public partial class GetById
{
    public record Query<TEntity> : IRequest<TEntity> where TEntity : EntityBase
    {
        public int Id { get; set; }
    }
}