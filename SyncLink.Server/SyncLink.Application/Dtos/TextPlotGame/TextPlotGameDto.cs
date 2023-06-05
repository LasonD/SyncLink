namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotGameDto
{
    public string Topic { get; set; } = null!;

    public int Id { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }

    public int GroupId { get; set; }

    public int CreatorId { get; set; }
    public GroupMemberDto Creator { get; set; } = null!;
}