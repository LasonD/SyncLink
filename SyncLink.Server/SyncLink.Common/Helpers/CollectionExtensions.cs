namespace SyncLink.Common.Helpers;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty(this IEnumerable<object>? source)
    {
        return source == null || !source.Any();
    }
}