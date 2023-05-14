using SyncLink.Application.Dtos;

namespace SyncLink.Application.Contracts.RealTime;

public interface INotificationsService
{
    Task NotifyMessageReceivedAsync(int groupId, MessageDto message, CancellationToken cancellationToken);
}