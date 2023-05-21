using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features.TextPlotGame;

public class TextPlotGame : EntityBase
{
    public int GroupId { get; set; }
    public Group Group { get; set; }
    public int StarterId { get; set; }
    public User Starter { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public IList<TextPlotEntry> Entries { get; set; } = new List<TextPlotEntry>();

    public TextPlotGame(Group group, User starter)
    {
        Group = group;
        Starter = starter;
        StartedAt = DateTime.UtcNow;
    }

    public void EndGame()
    {
        EndedAt = DateTime.UtcNow;
    }
}