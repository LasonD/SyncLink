using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos.WordsChainGame;
using SyncLink.Application.Exceptions;
using SyncLink.Application.Services;

namespace SyncLink.Application.UseCases.Features.TextChainGame.Commands;

public static class SendWordsChainEntry
{
    public class Command : IRequest<WordsChainGameEntryDto>
    {
        public int GroupId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Word { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, WordsChainGameEntryDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWordsChainGamesRepository _wordsChainGamesRepository;
        private readonly IAppDbContext _context;
        private readonly IWordsChainGameNotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly IWordCheckerService _wordCheckerService;

        public Handler(IAppDbContext context, IWordsChainGameNotificationService notificationService, IMapper mapper, IUserRepository userRepository, IWordCheckerService wordCheckerService, IWordsChainGamesRepository wordsChainGamesRepository)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
            _userRepository = userRepository;
            _wordCheckerService = wordCheckerService;
            _wordsChainGamesRepository = wordsChainGamesRepository;
        }

        public async Task<WordsChainGameEntryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var creatorResult = await _userRepository.GetUsersFromGroupAsync(request.GroupId, new[] { request.UserId }, cancellationToken);

            var sender = creatorResult.GetResult().Entities.Single();

            var isExistingWord = await _wordCheckerService.IsExistingWordAsync(request.Word, cancellationToken);

            if (!isExistingWord)
            {
                throw new BusinessException("Word is not existing");
            }

            var gameResult = await _wordsChainGamesRepository.GetByIdAsync(request.GameId, cancellationToken);

            var game = gameResult.GetResult();

            if (game.GroupId != request.GroupId)
            {
                throw new BusinessException("Game is not in the group");
            }

            var gameAlreadyHasWord = await _wordsChainGamesRepository.CheckGameAlreadyHasWordAsync(request.GameId, request.Word, cancellationToken);

            if (gameAlreadyHasWord)
            {
                throw new BusinessException("Game already has this word");
            }

            var isUserParticipant = await _wordsChainGamesRepository.CheckUserIsParticipantAsync(request.GameId, request.UserId, cancellationToken);

            var chainEntry = new WordsChainEntry
            {
                Word = request.Word.Trim().ToLowerInvariant(),
                GameId = request.GameId,
                ParticipantId = request.UserId,
            };

            if (!isUserParticipant)
            {
                var userToGame = new UserWordsChainGame
                {
                    UserId = request.UserId,
                    User = sender,
                    IsCreator = false,
                };

                await _context.UsersToWordsChains.AddAsync(userToGame, cancellationToken);
            }
            
            await _context.WordsChainEntries.AddAsync(chainEntry, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<WordsChainGameEntryDto>(chainEntry);

            await _notificationService.NotifyNewEntryAsync(request.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}