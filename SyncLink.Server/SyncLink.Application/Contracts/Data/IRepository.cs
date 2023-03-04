using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Data.Models;

namespace SyncLink.Application.Contracts.Data;

internal interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<RepositoryEntityResult<TEntity>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}