namespace SyncLink.Common.Helpers;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsNotNullOrWhiteSpace(this string? value)
    {
        return !value.IsNullOrWhiteSpace();
    }

    public static string GetValueOrDefault(this string? value, string defaultValue)
    {
        return value.IsNotNullOrWhiteSpace() ? value! : defaultValue;
    }
}