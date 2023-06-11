namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotGameWithEntriesDto : TextPlotGameDto
{
    public IList<TextPlotEntryDto> Entries { get; set; } = default!;
}