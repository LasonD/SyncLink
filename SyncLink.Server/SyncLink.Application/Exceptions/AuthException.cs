namespace SyncLink.Application.Exceptions;

public class AuthException : BusinessException
{
    public AuthException(IEnumerable<string>? errors)
    {
        Errors = errors;
    }

    public IEnumerable<string>? Errors { get; }
}