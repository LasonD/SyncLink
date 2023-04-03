namespace SyncLink.Application.Dtos;

public class GroupCompleteDto : GroupDto
{
    public IReadOnlyCollection<DomainUserDto> Members { get; set; } = null!;

    public IReadOnlyCollection<RoomDto> Rooms { get; set; } = null!;
}