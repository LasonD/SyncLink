using SyncLink.Common.Helpers;

namespace SyncLink.Common.Validation;

public static class ValidationExtensions
{
    public static void ThrowIfNull(this object value, string? parameterName = null)
    {
        if (value == null) throw new ArgumentNullException($"{parameterName ?? "Parameter"} cannot be null.");
    }

    public static void ThrowIfNullOrWhiteSpace(this string value, string? parameterName = null)
    {
        if (value.IsNullOrWhiteSpace()) throw new ArgumentException($"{parameterName ?? "Parameter"} cannot be null or white space.");
    }
}