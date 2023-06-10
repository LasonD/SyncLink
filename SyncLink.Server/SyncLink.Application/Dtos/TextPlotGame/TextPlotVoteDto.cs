namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotVoteDto : DtoBase
{
    public int UserId { get; set; }
    public GroupMemberDto User { get; set; } = null!;

    public string? Comment { get; set; }

    public int Score { get; set; }

    public int EntryId { get; set; }
}