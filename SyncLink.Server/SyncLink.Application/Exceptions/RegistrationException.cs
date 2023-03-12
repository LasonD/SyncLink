namespace SyncLink.Application.Exceptions;

public class RegistrationException : AuthException
{
    public RegistrationException(IEnumerable<string>? errors) : base(errors)
    {

    }
}