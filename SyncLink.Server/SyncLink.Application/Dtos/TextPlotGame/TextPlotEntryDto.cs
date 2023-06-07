namespace SyncLink.Application.Dtos.TextPlotGame;

public class TextPlotEntryDto : DtoBase
{
    public string Text { get; set; } = null!;

    public int GameId { get; set; }

    public int UserId { get; set; }
    public GroupMemberDto User { get; set; } = null!;

    public IList<TextPlotVoteDto> Votes { get; set; } = new List<TextPlotVoteDto>();
}