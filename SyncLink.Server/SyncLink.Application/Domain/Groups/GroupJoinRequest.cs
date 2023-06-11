using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain.Groups;

public class GroupJoinRequest : EntityBase
{
    public string? Message { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public GroupJoinRequestStatus Status { get; set; }
}

public enum GroupJoinRequestStatus
{
    Pending = 0,
    Accepted = 1,
    Rejected = 2,
}