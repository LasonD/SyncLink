using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Feed;

public class Voting : FeedItem
{
    public string Question { get; set; } = null!;

    public override FeedItemType Type => FeedItemType.Voting;

    public List<VotingOption> VotingOptions { get; set; } = null!;
}

public class VotingOption : EntityBase
{
    public string Text { get; set; } = null!;

    public List<Vote> Votes { get; set; } = null!;

    public int VotingId { get; set; }
    public Voting Voting { get; set; } = null!;
}

public class Vote : EntityBase
{
    public int VoterId { get; set; }
    public User Voter { get; set; } = null!;
}