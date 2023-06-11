using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Groups.Feed;

public class WordPhraseOfDayDiscussion : FeedItem
{
    public string WordPhrase { get; set; } = null!;
    public string? DescriptionOrQuestion { get; set; }
    public override FeedItemType Type => FeedItemType.Discussion;
}

public class DiscussionItem : EntityBase
{
    public string Text { get; set; } = null!;

    public int UpVotesCount { get; set; }
    public int DownVotesCount { get; set; }

    public int DiscussionId { get; set; }
    public WordPhraseOfDayDiscussion Discussion { get; set; } = null!;
}