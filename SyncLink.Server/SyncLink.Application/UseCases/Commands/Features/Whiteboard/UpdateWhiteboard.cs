using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.Features.Whiteboard;

public static class UpdateWhiteboard
{
    public record Command : IRequest<WhiteboardElementDto>
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public int WhiteboardId { get; set; }

        public WhiteboardElementDto Update { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, WhiteboardElementDto>
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

        public async Task<WhiteboardElementDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var authorResult = await _userRepository.GetUsersFromGroupAsync(request.GroupId, new[] { request.UserId }, cancellationToken);

            var author = authorResult.GetResult().Entities.Single();

            var whiteboard = (await _whiteboardRepository.GetByIdAsync(request.WhiteboardId, cancellationToken)).GetResult();

            var update = _mapper.Map<WhiteboardElement>(request.Update);

            update.Author = author;

            whiteboard.WhiteboardElements.Add(update);

            whiteboard.LastUpdatedTime = DateTime.UtcNow;

            await _whiteboardRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<WhiteboardElementDto>(update);

            return dto;
        }
    }
}