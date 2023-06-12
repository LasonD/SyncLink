namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotGameStatsDto
{
    public int GameId { get; set; }
    public int GroupId { get; set; }

    public string Topic { get; set; } = null!;

    public int EntriesCommittedCount { get; set; }
    public int WordsCommittedCount { get; set; }

    public List<TextPlotGameUserStatsDto> UserStats { get; set; } = default!;
}

public class TextPlotGameUserStatsDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public int EntriesCommittedCount { get; set; }
    public int EntriesSubmittedCount { get; set; }
    public int WordsCommittedCount { get; set; }
    public int WordsSubmittedCount { get; set; }
    public int TotalReceivedScore { get; set; }
    public int VotesLeftCount { get; set; }
    public List<string> CommentsReceived { get; set; } = default!;
}