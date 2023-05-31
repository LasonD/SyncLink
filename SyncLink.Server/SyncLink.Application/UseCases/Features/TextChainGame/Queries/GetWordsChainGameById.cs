using AutoMapper;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos.WordsChainGame;
using SyncLink.Application.UseCases.Queries.GetById.Base;

namespace SyncLink.Application.UseCases.Features.TextChainGame.Queries;

public class GetWordsChainGameById
{
    public record Query : GetById.Query<WordsChainGame, WordsChainGameDto>
    {
        public int GroupId { get; set; }
    };

    public class Handler : GetById.Handler<WordsChainGame, WordsChainGameDto>
    {
        private readonly IUserRepository _userRepository;

        public Handler(IWordsChainGamesRepository wordsChainGamesRepository, IMapper mapper, IUserRepository userRepository) : base(wordsChainGamesRepository, mapper)
        {
            _userRepository = userRepository;
        }

        protected override Task<bool> CheckUserHasAccessAsync(GetById.Query<WordsChainGame, WordsChainGameDto> request, CancellationToken cancellationToken)
        {
            if (!(request is Query query))
            {
                throw new InvalidOperationException();
            }

            if (query.UserId == null)
            {
                return Task.FromResult(false);
            }

            return _userRepository.IsUserInGroupAsync(query.UserId.Value, query.GroupId, cancellationToken);
        }
    }
}