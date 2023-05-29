using AutoMapper;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;
using SyncLink.Application.UseCases.Queries.GetById.Base;

namespace SyncLink.Application.UseCases.Features.Whiteboard.Queries;

public class GetWhiteboardById
{
    public record Query : GetById.Query<Domain.Features.Whiteboard, WhiteboardDto>
    {
        public int GroupId { get; set; }
    };

    public class Handler : GetById.Handler<Domain.Features.Whiteboard, WhiteboardDto>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IWhiteboardRepository whiteboardRepository, IMapper mapper, IUserRepository userRepository) : base(whiteboardRepository, mapper)
        {
            _userRepository = userRepository;
        }

        protected override Task<bool> CheckUserHasAccessAsync(UseCases.Queries.GetById.Base.GetById.Query<Domain.Features.Whiteboard, WhiteboardDto> request, CancellationToken cancellationToken)
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