namespace SyncLink.Application.Contracts.Data.Result;

public class RepositoryError
{
    public RepositoryError(string description, string? code = default, bool isClientFacing = true)
    {
        Description = description;
        Code = code;
        IsClientFacing = isClientFacing;
    }

    public string Description { get; }
    public string? Code { get; }
    public bool IsClientFacing { get; }

    public static implicit operator string(RepositoryError d) => d.ToString();

    public static implicit operator RepositoryError(string d) => new(d);

    public override string ToString()
    {
        var codePart = !string.IsNullOrEmpty(Code) ? $"{Code} : " : string.Empty;
        return $"{codePart}{Description}";
    }
}