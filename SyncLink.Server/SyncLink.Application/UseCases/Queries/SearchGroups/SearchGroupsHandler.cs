using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Dtos;

namespace SyncLink.Application.UseCases.Queries.SearchGroups;

public partial class SearchGroups
{
    public class Handler : IRequestHandler<Query, IPaginatedEnumerable<GroupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGroupsRepository _groupsRepository;

        public Handler(IGroupsRepository groupsRepository, IMapper mapper)
        {
            _groupsRepository = groupsRepository;
            _mapper = mapper;
        }

        public async Task<IPaginatedEnumerable<GroupDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var searchTerms = request.SearchQuery?.Split(' ') ?? new[] { string.Empty };

            var searchResult = await _groupsRepository.SearchByNameAndDescriptionAsync(searchTerms, cancellationToken);

            var foundGroups = searchResult.GetResult();

            var paginatedDto = _mapper.Map<IPaginatedEnumerable<GroupDto>>(foundGroups);

            return paginatedDto;
        }
    }
}