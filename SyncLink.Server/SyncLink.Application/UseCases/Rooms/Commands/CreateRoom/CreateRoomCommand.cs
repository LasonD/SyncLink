using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Rooms.Commands.CreateRoom;

public partial class CreateRoom
{
    public record Command : IRequest<RoomDto>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<int> MemberIds { get; set; } = Enumerable.Empty<int>();
    }
}