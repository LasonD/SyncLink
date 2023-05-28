using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class StartGame
{
    public class StartGameCommand : IRequest<TextPlotGameDto>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<StartGameCommand, TextPlotGameDto>
    {
        private readonly IAppDbContext _context;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext context, ITextPlotGameNotificationService notificationService, IMapper mapper)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<TextPlotGameDto> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FindAsync(request.GroupId, cancellationToken);
            var starter = await _context.ApplicationUsers.FindAsync(request.UserId, cancellationToken);

            var game = new Domain.Features.TextPlotGame.TextPlotGame(group, starter);

            _context.TextPlotGames.Add(game);
            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyGameStartedAsync(group.Id, game, cancellationToken);

            var dto = _mapper.Map<TextPlotGameDto>(game);

            return dto;
        }
    }
}