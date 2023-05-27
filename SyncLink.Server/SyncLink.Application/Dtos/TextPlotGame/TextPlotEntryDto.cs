using SyncLink.Application.Domain;

namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotEntryDto
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public int GameId { get; set; }
    public Domain.Features.TextPlotGame.TextPlotGame Game { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public IList<TextPlotVoteDto> Votes { get; set; } = new List<TextPlotVoteDto>();
}