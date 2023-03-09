namespace SyncLink.Application.Exceptions;

public class LoginException : AuthException
{
    public LoginException(IEnumerable<string>? errors) : base(errors)
    {
    }
}