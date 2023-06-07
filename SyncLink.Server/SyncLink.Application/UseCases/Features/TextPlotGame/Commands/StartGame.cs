using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class StartGame
{
    public class Command : IRequest<TextPlotGameDto>
    {
        public string Topic { get; set; } = null!;
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotGameDto>
    {
        private readonly IUserRepository _usersRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly ITextPlotGameRepository _textPlotGamesRepository;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(ITextPlotGameNotificationService notificationService, IMapper mapper, IUserRepository usersRepository, IGroupsRepository groupsRepository, ITextPlotGameRepository textPlotGamesRepository)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _usersRepository = usersRepository;
            _groupsRepository = groupsRepository;
            _textPlotGamesRepository = textPlotGamesRepository;
        }

        public async Task<TextPlotGameDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var group = (await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken)).GetResult();
            var starter = (await _usersRepository.GetByIdAsync(request.UserId, cancellationToken)).GetResult();

            var game = new Domain.Features.TextPlotGame.TextPlotGame(group, starter, request.Topic);

            await _textPlotGamesRepository.CreateAsync(game, cancellationToken);
            await _textPlotGamesRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TextPlotGameDto>(game);

            await _notificationService.NotifyGameStartedAsync(group.Id, dto, cancellationToken);

            return dto;
        }
    }
}