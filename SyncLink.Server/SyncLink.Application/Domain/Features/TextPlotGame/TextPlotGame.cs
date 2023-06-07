using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features.TextPlotGame;

public class TextPlotGame : EntityBase
{
    public string Topic { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; }
    public int? CreatorId { get; set; }
    public User Creator { get; set; }
    public DateTime? EndedAt { get; set; }
    public IList<TextPlotEntry> Entries { get; set; } = new List<TextPlotEntry>();

    protected TextPlotGame() { }

    public TextPlotGame(Group group, User creator, string topic)
    {
        Group = group;
        Creator = creator;
        Topic = topic;
    }

    public TextPlotEntry AddEntry(User sender, string text)
    {
        var entry = new TextPlotEntry(sender, this, text);
        Entries.Add(entry);

        return entry;
    }

    public void EndGame()
    {
        EndedAt = DateTime.UtcNow;
    }
}