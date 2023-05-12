using SyncLink.Application.Contracts.Data.Result.Pagination;

namespace SyncLink.Application.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IPaginatedEnumerable<MessageDto> Messages { get; set; } = null!;
}