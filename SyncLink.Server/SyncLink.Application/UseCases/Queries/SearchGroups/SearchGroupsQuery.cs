using MediatR;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Queries.SearchGroups;

public partial class SearchGroups
{
    public record Query(int UserId, string? SearchQuery, bool OnlyMembership) : IRequest<IPaginatedEnumerable<GroupDto>>;
}