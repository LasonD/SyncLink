namespace SyncLink.Application.Dtos;

public record GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}