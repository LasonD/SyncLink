namespace SyncLink.Application.Dtos.WordsChainGame;

public class WordsChainGameEntryDto
{
    public int Id { get; set; }
    public string Word { get; set; } = null!;
    public int GameId { get; set; }
    public int SenderId { get; set; }
}