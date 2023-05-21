namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotEntryDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}