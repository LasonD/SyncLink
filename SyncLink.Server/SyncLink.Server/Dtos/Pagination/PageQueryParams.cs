namespace SyncLink.Server.Dtos.Pagination;

public record PageQueryParams(int PageNumber = 1, int PageSize = 100_000);