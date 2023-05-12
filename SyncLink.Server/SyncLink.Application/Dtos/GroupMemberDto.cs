namespace SyncLink.Application.Dtos;

public class GroupMemberDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public bool IsCreator { get; set; }
    public bool IsAdmin { get; set; }
}