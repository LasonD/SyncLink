using MediatR;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Commands.CreateRoom;

public partial class CreateRoom
{
    public record Command : IRequest<RoomDto>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<int> UserIds { get; set; } = Enumerable.Empty<int>();
    }
}