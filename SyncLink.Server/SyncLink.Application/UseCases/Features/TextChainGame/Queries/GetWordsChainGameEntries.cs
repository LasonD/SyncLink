using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos.WordsChainGame;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Features.TextChainGame.Queries;

public static class GetWordsChainGameEntries
{
    public class Query : IRequest<IPaginatedResult<WordsChainGameEntryDto>>
    {
        public int GroupId { get; init; }
        public int GameId { get; init; }
        public int UserId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<WordsChainGameEntryDto>>
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

        public async Task<IPaginatedResult<WordsChainGameEntryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var wordChainEntriesResult = await _wordsChainGamesRepository.GetWordsChainGameEntriesAsync(
                request.GroupId,
                request.GameId,
                new OrderedPaginationQuery<WordsChainEntry>(request.PageNumber, request.PageSize),
                cancellationToken);

            var whiteboards = wordChainEntriesResult.GetResult();

            var dto = _mapper.Map<PaginatedResult<WordsChainGameEntryDto>>(whiteboards);

            return dto;
        }
    }
}