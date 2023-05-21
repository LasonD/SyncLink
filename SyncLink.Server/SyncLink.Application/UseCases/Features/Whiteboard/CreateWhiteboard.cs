using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Features.Whiteboard;

public class CreateWhiteboard
{
    public record Command : IRequest<WhiteboardDto>
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public string Name { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, WhiteboardDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IWhiteboardRepository _whiteboardRepository;

        public Handler(IMapper mapper, IUserRepository userRepository, IWhiteboardRepository whiteboardRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _whiteboardRepository = whiteboardRepository;
        }

        public async Task<WhiteboardDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var creatorResult = await _userRepository.GetUsersFromGroupAsync(request.GroupId, new[] { request.UserId }, cancellationToken);

            var sender = creatorResult.GetResult().Entities.Single();

            var whiteboard = new Domain.Features.Whiteboard
            {
                Name = request.Name,
                GroupId = request.GroupId,
                OwnerId = request.UserId,
                Owner = sender,
            };

            await _whiteboardRepository.CreateAsync(whiteboard, cancellationToken);
            await _whiteboardRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<WhiteboardDto>(whiteboard);

            return dto;
        }
    }
}