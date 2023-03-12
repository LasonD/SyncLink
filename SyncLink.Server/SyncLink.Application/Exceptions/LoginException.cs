namespace SyncLink.Application.Exceptions;

public class LoginException : AuthException
{
    public LoginException(ICollection<string>? errors) : base(errors)
    {

    }
}