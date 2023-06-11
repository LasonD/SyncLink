using SyncLink.Application.Domain.Groups;

namespace SyncLink.Server.Dtos;

public class UpdateGroupJoinRequestDto
{
    public GroupJoinRequestStatus NewStatus { get; set; }
}