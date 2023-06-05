using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class SubmitEntry
{
    public class Command : IRequest<TextPlotEntryDto>
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Text { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, TextPlotEntryDto>
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

        public async Task<TextPlotEntryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var game = await _context.TextPlotGames.FindAsync(request.GameId, cancellationToken);
            var user = await _context.ApplicationUsers.FindAsync(request.UserId, cancellationToken);

            var entry = new TextPlotEntry(user, game, request.Text);

            _context.TextPlotEntries.Add(entry);
            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyNewEntryAsync(game.GroupId, entry, cancellationToken);

            var dto = _mapper.Map<TextPlotEntryDto>(entry);

            return dto;
        }
    }
}