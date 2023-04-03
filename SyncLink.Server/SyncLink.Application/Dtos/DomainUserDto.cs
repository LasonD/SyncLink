namespace SyncLink.Application.Dtos;

public class DomainUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public bool IsCreator { get; set; }
    public bool IsAdmin { get; set; }
}