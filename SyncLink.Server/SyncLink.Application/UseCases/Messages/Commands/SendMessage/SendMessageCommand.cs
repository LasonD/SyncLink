using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.SendMessage;

public partial class SendMessage
{
    public record Command : IRequest<MessageDto>
    {
        public int SenderId { get; set; }

        public int GroupId { get; set; }

        public string Text { get; set; } = null!;

        public int? RoomId { get; set; }

        public int? RecipientId { get; set; }
    }
}