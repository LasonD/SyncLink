namespace SyncLink.Server.Dtos;

public class SendMessageDto
{
    public int GroupId { get; set; }

    public string Text { get; set; } = null!;

    public int? RoomId { get; set; }

    public int? RecipientId { get; set; }
}