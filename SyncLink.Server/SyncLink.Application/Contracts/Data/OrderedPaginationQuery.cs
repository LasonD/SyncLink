namespace Application.Application.Queries
{
    public class OrderedPaginationQuery
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 10;

        public int Page { get; set; } = DefaultPage;

        public int PageSize { get; set; } = DefaultPageSize;

        public string? OrderBy { get; set; }

        public string? ThenBy { get; set; }
    }
}
