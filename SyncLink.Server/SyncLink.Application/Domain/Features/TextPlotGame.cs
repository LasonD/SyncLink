using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features;

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

public class TextPlotEntry : EntityBase
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int GameId { get; set; }
    public TextPlotGame Game { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<TextPlotVote> Votes { get; set; } = new List<TextPlotVote>();

    public TextPlotEntry(User user, TextPlotGame game, string text)
    {
        User = user;
        UserId = user.Id;
        Game = game;
        GameId = game.Id;
        Text = text;
        CreatedAt = DateTime.UtcNow;
    }
}


public class TextPlotVote : EntityBase
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int EntryId { get; set; }
    public TextPlotEntry Entry { get; set; }

    public TextPlotVote(User user, TextPlotEntry entry)
    {
        User = user;
        Entry = entry;
    }
}