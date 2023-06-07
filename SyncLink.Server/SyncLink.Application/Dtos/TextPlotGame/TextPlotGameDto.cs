namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotGameDto : DtoBase
{
    public string Topic { get; set; } = null!;
    public DateTime? EndedAt { get; set; }

    public int GroupId { get; set; }

    public int CreatorId { get; set; }
    public GroupMemberDto Creator { get; set; } = null!;
}