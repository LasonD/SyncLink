namespace SyncLink.Server.Dtos.Pagination;

public record struct PageQueryParams(int PageNumber = 1, int PageSize = 100_000);