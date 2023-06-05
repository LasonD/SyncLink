namespace SyncLink.Application.UseCases.Queries;

public class QueryWithPagination
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 100_000;
}