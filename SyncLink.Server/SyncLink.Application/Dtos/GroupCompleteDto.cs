namespace SyncLink.Application.Dtos;

public class GroupDto : GroupOverviewDto
{
    public IReadOnlyCollection<DomainUserDto> Members { get; set; } = null!;

    public IReadOnlyCollection<RoomDto> Rooms { get; set; } = null!;
}