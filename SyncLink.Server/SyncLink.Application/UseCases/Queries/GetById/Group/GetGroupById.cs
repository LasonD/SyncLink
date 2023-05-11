using AutoMapper;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Queries.GetById.Group;

public partial class GetGroupById
{
    public record Query : Base.GetById.Query<Domain.Group, GroupDto>;

    public class Handler : Base.GetById.Handler<Domain.Group, GroupDto>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IEntityRepository<Domain.Group> genericRepository, IMapper mapper, IUserRepository userRepository) : base(genericRepository, mapper)
        {
            _userRepository = userRepository;
        }

        // protected override Task<bool> CheckUserHasAccessAsync(Base.GetById.Query<Domain.Group, GroupDto> request, CancellationToken cancellationToken)
        // {
        //     if (request.UserId == null)
        //     {
        //         return false;
        //     }
        //
        //     var user = _userRepository.
        // }
    }
}