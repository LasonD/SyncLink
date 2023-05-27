using SyncLink.Application.Domain;

namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotGameDto
{
    public int Id { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int CreatorId { get; set; }
    public User Creator { get; set; } = null!;

    public IList<TextPlotEntryDto> Entries { get; set; } = new List<TextPlotEntryDto>();
}