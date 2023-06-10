using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features.TextPlotGame;

public class TextPlotVote : EntityBase
{
    public int? UserId { get; set; }
    public User User { get; set; }
    public int EntryId { get; set; }
    public TextPlotEntry Entry { get; set; }

    public int Score { get; set; }
    public string? Comment { get; set; }

    protected TextPlotVote() { }

    public TextPlotVote(User user, TextPlotEntry entry, string? comment, int score)
    {
        User = user;
        Entry = entry;
        Comment = comment;

        if (score is < 0 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 10");
        }

        Score = score;
    }
}