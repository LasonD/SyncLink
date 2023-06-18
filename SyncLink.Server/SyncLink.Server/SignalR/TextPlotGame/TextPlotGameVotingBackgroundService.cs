using SyncLink.Application.Contracts.RealTime;
using SyncLink.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using MediatR;
using SyncLink.Application.UseCases.Features.TextPlotGame.Commands;
using SyncLink.Server.Helpers;

namespace SyncLink.Server.SignalR.TextPlotGame;

public class TextPlotGameVotingBackgroundService : BackgroundService, ITextPlotGameVotingProgressNotifier
{
    private readonly object _lock = new();
    private readonly ConcurrentDictionary<int, CancellationTokenSource> _gameTimers = new();
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TextPlotGameVotingBackgroundService(IHubContext<SyncLinkHub, ISyncLinkHub> hubContext, IServiceScopeFactory serviceScopeFactory)
    {
        _hubContext = hubContext;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public void StartGameTimerIfNotYetStarted(int groupId, int gameId, TimeSpan gameDuration)
    {
        lock (_lock)
        {
            if (IsGameTimerRunning(gameId))
            {
                return;
            }

            var cts = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                var totalSeconds = gameDuration.TotalSeconds;

                for (var secondsElapsed = gameDuration.TotalSeconds; secondsElapsed >= 0; secondsElapsed--)
                {
                    if (cts.Token.IsCancellationRequested)
                        break;

                    var progressPercent = secondsElapsed / totalSeconds * 100;

                    await _hubContext.Clients.Group(HubHelper.GetGroupNameForGroupId(groupId)).VotingTimerProgress(gameId, progressPercent);

                    await Task.Delay(1000, cts.Token);
                }

                if (cts.Token.IsCancellationRequested)
                {
                    await _hubContext.Clients.Group(HubHelper.GetGroupNameForGroupId(groupId)).EntryNotCommitted(gameId);
                }
                else
                {
                    await CommitGameEntryAsync(groupId, gameId, cts);
                }

                _gameTimers.TryRemove(gameId, out _);

            }, cts.Token);

            _gameTimers[gameId] = cts;
        }
    }

    private bool IsGameTimerRunning(int gameId)
    {
        return _gameTimers.ContainsKey(gameId);
    }

    private async Task CommitGameEntryAsync(int groupId, int gameId, CancellationTokenSource cts)
    {
        var command = new CommitEntry.Command
        {
            GameId = gameId,
            GroupId = groupId
        };

        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command, cts.Token);
    }

    public void StopGameTimer(int gameId)
    {
        if (!_gameTimers.TryGetValue(gameId, out var cts))
        {
            return;
        }

        cts.Cancel();
        _gameTimers.TryRemove(gameId, out _);
    }
}
