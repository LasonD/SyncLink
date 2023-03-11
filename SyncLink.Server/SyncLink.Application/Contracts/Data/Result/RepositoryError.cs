namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryError
{
    public RepositoryError(string description, string? code = default)
    {
        Description = description;
        Code = code;
    }

    public string Description { get; }
    public string? Code { get; }

    public static implicit operator string(RepositoryError d) => d.ToString();

    public override string ToString()
    {
        var codePart = !string.IsNullOrEmpty(Code) ? $"{Code} : " : string.Empty;
        return $"{codePart}{Description}";
    }
}