namespace SyncLink.Application.Contracts.RealTime;

public interface ITextPlotGameVotingProgressNotifier
{
    void StartGameTimer(int groupId, int gameId, TimeSpan gameDuration);
    void StopGameTimer(int gameId);
}