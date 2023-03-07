namespace SyncLink.Application.Contracts.Data.Result
{
    public class RepositoryError
    {
        public RepositoryError(string description, string? code = default)
        {
            Description = description;
            Code = code;
        }

        public string Description { get; }
        public string? Code { get; }
    }
}
