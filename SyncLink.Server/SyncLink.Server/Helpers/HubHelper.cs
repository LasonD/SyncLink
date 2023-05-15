namespace SyncLink.Server.Helpers;

public static class HubHelper
{
    public static string GetGroupNameForGroupId(int groupId)
    {
        return $"Group_{groupId}";
    }
}