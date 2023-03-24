using MediatR;
using SyncLink.Application.Contracts.Data.Enums;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Queries.SearchGroups;

public partial class SearchGroups
{
    public record Query(int UserId, string? SearchQuery, GroupSearchMode SearchMode) : IRequest<IPaginatedEnumerable<GroupDto>>;
}