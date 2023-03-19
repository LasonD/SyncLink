namespace SyncLink.Common.Helpers;

public static class DefaultHelpers
{
    public static T? FirstNonDefault<T>(params T[] values)
    {
        return values.FirstOrDefault(x => EqualityComparer<T>.Default.Equals(x, default));
    }
}