namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotVoteDto
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public GroupMemberDto User { get; set; } = null!;

    public int EntryId { get; set; }
}