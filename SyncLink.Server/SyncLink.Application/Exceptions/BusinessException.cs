namespace SyncLink.Application.Exceptions;

public class BusinessException : Exception
{
    public const string DefaultClientErrorMessage = "Something went wrong.";

    public string ClientFacingErrorMessage { get; private set; } = DefaultClientErrorMessage;
}