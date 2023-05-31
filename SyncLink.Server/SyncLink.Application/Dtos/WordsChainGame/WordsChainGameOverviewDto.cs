namespace SyncLink.Application.Dtos.WordsChainGame;

public class WordsChainGameOverviewDto
{
    public int Id { get; set; }
    public string Topic { get; set; } = null!;
    public int ParticipantsCount { get; set; }
    public int WordsCount { get; set; }
}