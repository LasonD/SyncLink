namespace SyncLink.Server.Dtos;

public class CreateRoomDto
{
    public int GroupId { get; set; }

    public int[] MembersId { get; set; } = null!;

    public string? Name { get; set; }

    public bool IsPrivate { get; set; }
}