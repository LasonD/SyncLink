namespace SyncLink.Common.Helpers;

public static class CollectionHelpers
{
    public static bool IsNullOrEmpty(this IEnumerable<object>? source)
    {
        return source == null || !source.Any();
    }

    public static List<T> WrapInList<T>(T value)
    {
        return new List<T> { value };
    }
}