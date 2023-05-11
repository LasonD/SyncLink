using AutoMapper;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Queries.GetById.GroupComplete;

public partial class GetGroupCompleteById
{
    public record Query : Base.GetById.Query<Domain.Group, GroupDto>;

    public class Handler : Base.GetById.Handler<Domain.Group, GroupDto>
    {
        public Handler(IEntityRepository<Domain.Group> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}