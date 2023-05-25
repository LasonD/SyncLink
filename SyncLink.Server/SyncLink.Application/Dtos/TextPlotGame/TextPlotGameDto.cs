namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotGameDto
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int StarterId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public IList<TextPlotEntryDto> Entries { get; set; } = new List<TextPlotEntryDto>();
}