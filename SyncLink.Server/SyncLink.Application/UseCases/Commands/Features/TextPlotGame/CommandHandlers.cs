using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Application.UseCases.Commands.Features.TextPlotGame;

public class StartGameCommandHandler : IRequestHandler<StartGameCommand, Domain.Features.TextPlotGame>
{
    private readonly IAppDbContext _context;
    private readonly ITextPlotGameNotificationService _notificationService;

    public StartGameCommandHandler(IAppDbContext context, ITextPlotGameNotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<Domain.Features.TextPlotGame> Handle(StartGameCommand request, CancellationToken cancellationToken)
    {
        var group = await _context.Groups.FindAsync(request.GroupId, cancellationToken);
        var starter = await _context.ApplicationUsers.FindAsync(request.StarterId, cancellationToken);

        var game = new Domain.Features.TextPlotGame(group, starter);

        _context.TextPlotGames.Add(game);
        await _context.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyGameStartedAsync(group.Id, game, cancellationToken);

        return game;
    }
}

public class SubmitEntryCommandHandler : IRequestHandler<SubmitEntryCommand, TextPlotEntry>
{
    private readonly IAppDbContext _context;
    private readonly ITextPlotGameNotificationService _notificationService;

    public SubmitEntryCommandHandler(IAppDbContext context, ITextPlotGameNotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<TextPlotEntry> Handle(SubmitEntryCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.TextPlotGames.FindAsync(request.GameId, cancellationToken);
        var user = await _context.ApplicationUsers.FindAsync(request.UserId, cancellationToken);

        var entry = new TextPlotEntry(user, game, request.Text);

        _context.TextPlotEntries.Add(entry);
        await _context.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyNewEntryAsync(game.GroupId, entry, cancellationToken);

        return entry;
    }
}

public class VoteEntryCommandHandler : IRequestHandler<VoteEntryCommand>
{
    private readonly IAppDbContext _context;
    private readonly ITextPlotGameNotificationService _notificationService;

    public VoteEntryCommandHandler(IAppDbContext context, ITextPlotGameNotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task Handle(VoteEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = await _context.TextPlotEntries.FindAsync(request.EntryId, cancellationToken);
        var voter = await _context.ApplicationUsers.FindAsync(request.UserId, cancellationToken);

        var vote = new TextPlotVote(voter, entry);

        _context.TextPlotVotes.Add(vote);
        await _context.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyVoteReceivedAsync(entry.Game.GroupId, vote, cancellationToken);
    }
}

public class EndGameCommandHandler : IRequestHandler<EndGameCommand>
{
    private readonly IAppDbContext _context;
    private readonly ITextPlotGameNotificationService _notificationService;

    public EndGameCommandHandler(IAppDbContext context, ITextPlotGameNotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task Handle(EndGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.TextPlotGames.FindAsync(request.GameId, cancellationToken);

        game.EndGame();

        await _context.SaveChangesAsync(cancellationToken);

        await _notificationService.NotifyGameEndedAsync(game.GroupId, game, cancellationToken);
    }
}
