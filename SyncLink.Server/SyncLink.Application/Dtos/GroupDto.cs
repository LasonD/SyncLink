namespace SyncLink.Application.Dtos;

public class GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public int MembersCount { get; set; }
}