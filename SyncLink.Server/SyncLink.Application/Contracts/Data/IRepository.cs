using SyncLink.Data.Models;

namespace SyncLink.Application.Contracts.Data
{
    internal interface IRepository<TEntity> where TEntity : EntityBase
    {
    }
}
