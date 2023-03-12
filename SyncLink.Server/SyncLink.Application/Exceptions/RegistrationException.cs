namespace SyncLink.Application.Exceptions;

public class RegistrationException : AuthException
{
    public RegistrationException(ICollection<string>? errors) : base(errors)
    {

    }
}