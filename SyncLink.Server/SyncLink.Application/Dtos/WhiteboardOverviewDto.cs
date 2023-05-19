namespace SyncLink.Application.Dtos;

public class WhiteboardOverviewDto
{
    public string Name { get; set; } = null!;

    public int OwnerId { get; set; }

    public int GroupId { get; set; }

    public DateTime LastUpdatedTime { get; set; }
}