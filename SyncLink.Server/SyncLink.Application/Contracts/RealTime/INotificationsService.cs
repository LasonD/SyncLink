using SyncLink.Application.Dtos;

namespace SyncLink.Application.Contracts.RealTime;

public interface INotificationsService
{
    Task NotifyMessageReceivedAsync(int groupId, int? roomId, int? otherUserId, bool isPrivate, MessageDto message, CancellationToken cancellationToken);
}