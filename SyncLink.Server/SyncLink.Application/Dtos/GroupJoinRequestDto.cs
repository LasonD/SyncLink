namespace SyncLink.Application.Dtos;

public class GroupJoinRequestDto : DtoBase
{
    public string? Message { get; set; }
    public int UserId { get; set; }
    public int GroupId { get; set; }
    public string Status { get; set; } = null!;
}