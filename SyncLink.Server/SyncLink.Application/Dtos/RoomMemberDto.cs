namespace SyncLink.Application.Dtos;

public class RoomMemberDto
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public bool IsAdmin { get; set; }
}