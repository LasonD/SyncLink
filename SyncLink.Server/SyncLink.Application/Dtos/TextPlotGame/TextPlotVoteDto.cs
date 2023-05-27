using SyncLink.Application.Domain;

namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotVoteDto
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int EntryId { get; set; }
    public TextPlotEntryDto Entry { get; set; } = null!;    
}