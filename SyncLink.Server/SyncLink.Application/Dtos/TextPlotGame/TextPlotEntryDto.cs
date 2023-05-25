namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotEntryDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public IList<TextPlotVoteDto> Votes { get; set; } = new List<TextPlotVoteDto>();
}