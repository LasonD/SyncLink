namespace SyncLink.Server.Dtos;

public class CreateRoomDto
{
    public int GroupId { get; set; }

    public int[] MemberIds { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }
}