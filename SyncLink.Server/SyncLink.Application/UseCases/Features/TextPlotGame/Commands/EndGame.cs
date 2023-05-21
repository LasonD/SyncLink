using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class EndGame
{
    public class Command : IRequest<TextPlotGameDto>
    {
        public int GameId { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotGameDto>
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

        public async Task<TextPlotGameDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var game = await _context.TextPlotGames.FindAsync(request.GameId, cancellationToken);

            game.EndGame();

            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyGameEndedAsync(game.GroupId, game, cancellationToken);

            var dto = _mapper.Map<TextPlotGameDto>(game);

            return dto;
        }
    }

}