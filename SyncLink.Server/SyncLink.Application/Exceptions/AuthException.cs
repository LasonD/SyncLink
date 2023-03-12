using SyncLink.Common.Helpers;
using System.Text;

namespace SyncLink.Application.Exceptions;

public class AuthException : BusinessException
{
    public AuthException(ICollection<string>? errors) : base(BuildFormattedErrorMessage(errors))
    {
        Errors = errors;
    }

    public IEnumerable<string>? Errors { get; }

    private static string BuildFormattedErrorMessage(ICollection<string>? errors)
    {
        var messageBuilder = new StringBuilder("An authorization related error occurred");

        if (errors.IsNullOrEmpty())
        {
            return messageBuilder.ToString();
        }

        foreach (var error in errors)
        {
            messageBuilder.Append(error);
        }

        if (messageBuilder[^1] != '.')
        {
            messageBuilder.Append('.');
        }

        return messageBuilder.ToString();
    }
}