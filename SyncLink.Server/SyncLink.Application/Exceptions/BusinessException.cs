namespace SyncLink.Application.Exceptions;

public class BusinessException : Exception
{
    public string ClientFacingErrorMessage { get; private set; }
}