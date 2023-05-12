namespace SyncLink.Application.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsPrivate { get; set; }
}