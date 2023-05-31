using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Exceptions;
using SyncLink.Application.Dtos.WordsChainGame;

namespace SyncLink.Application.UseCases.Features.TextChainGame.Queries;

public static class GetWordsChainGamesOverview
{
    public class Query : IRequest<IPaginatedResult<WordsChainGameOverviewDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<WordsChainGameOverviewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly IWordsChainGamesRepository _wordsChainGamesRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository, IWordsChainGamesRepository wordsChainGamesRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _wordsChainGamesRepository = wordsChainGamesRepository;
        }

        public async Task<IPaginatedResult<WordsChainGameOverviewDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var wordChainGamesResult = await _wordsChainGamesRepository.GetGroupWordsChainGamesAsync(
                request.GroupId,
                new OrderedPaginationQuery<WordsChainGame>(request.PageNumber, request.PageSize),
                cancellationToken);

            var whiteboards = wordChainGamesResult.GetResult();

            var dto = _mapper.Map<PaginatedResult<WordsChainGameOverviewDto>>(whiteboards);

            foreach (var d in dto.Entities)
            {
                d.WordsCount = await _wordsChainGamesRepository.CountGameEntriesAsync(d.Id, cancellationToken);
                d.ParticipantsCount = await _wordsChainGamesRepository.CountGameParticipantsAsync(d.Id, cancellationToken);
            }

            return dto;
        }
    }
}