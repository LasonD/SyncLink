using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups;

namespace SyncLink.Application.Domain.Groups.Feed;

public abstract class FeedItem : EntityBase
{
    public abstract FeedItemType Type { get; }

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public int? AuthorId { get; set; }
    public User? Author { get; set; }
}

public enum FeedItemType
{
    Discussion,
    Voting,
    WordsQuiz,
}