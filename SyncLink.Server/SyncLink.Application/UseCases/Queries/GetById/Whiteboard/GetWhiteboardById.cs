using AutoMapper;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Queries.GetById.Whiteboard;

public class GetWhiteboardById
{
    public record Query : Base.GetById.Query<Domain.Features.Whiteboard, WhiteboardDto>
    {
        public int GroupId { get; set; }
    };

    public class Handler : Base.GetById.Handler<Domain.Features.Whiteboard, WhiteboardDto>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IEntityRepository<Domain.Features.Whiteboard> genericRepository, IMapper mapper, IUserRepository userRepository) : base(genericRepository, mapper)
        {
            _userRepository = userRepository;
        }

        protected override Task<bool> CheckUserHasAccessAsync(Base.GetById.Query<Domain.Features.Whiteboard, WhiteboardDto> request, CancellationToken cancellationToken)
        {
            if (!(request is Query query))
            {
                throw new InvalidOperationException();
            }

            if (query.UserId == null)
            {
                return Task.FromResult(false);
            }

            return _userRepository.IsUserInGroupAsync(query.UserId.Value, query.GroupId, cancellationToken);
        }
    }
}