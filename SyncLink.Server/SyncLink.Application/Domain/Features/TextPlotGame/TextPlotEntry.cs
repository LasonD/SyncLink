using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features.TextPlotGame;

public class TextPlotEntry : EntityBase
{
    public int? UserId { get; set; }
    public User User { get; set; }
    public int GameId { get; set; }
    public TextPlotGame Game { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public IList<TextPlotVote> Votes { get; set; } = new List<TextPlotVote>();
    public bool IsCommitted { get; set; } = false;

    protected TextPlotEntry() { }

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