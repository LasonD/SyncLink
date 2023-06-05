using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Features.TextPlotGame;

public class TextPlotVote : EntityBase
{
    public int? UserId { get; set; }
    public User User { get; set; }
    public int EntryId { get; set; }
    public TextPlotEntry Entry { get; set; }

    public string? Comment { get; set; }

    protected TextPlotVote() { }

    public TextPlotVote(User user, TextPlotEntry entry, string? comment)
    {
        User = user;
        Entry = entry;
        Comment = comment;
    }
}