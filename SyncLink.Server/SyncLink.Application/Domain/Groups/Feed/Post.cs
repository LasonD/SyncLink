using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Domain.Groups.Rooms;

namespace SyncLink.Application.Domain.Groups.Feed;

public class Post : EntityBase
{
    public string Text { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int? RoomId { get; set; }
    public Room? Room { get; set; }
    public int? GroupId { get; set; }
    public Group? Group { get; set; }
    public int? WhiteboardId { get; set; }
}