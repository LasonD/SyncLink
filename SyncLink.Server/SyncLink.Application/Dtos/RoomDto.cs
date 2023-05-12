namespace SyncLink.Application.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IReadOnlyCollection<GroupMemberDto> Members { get; set; } = null!;
}