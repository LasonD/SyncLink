using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos.WordsChainGame;

namespace SyncLink.Application.UseCases.Features.TextChainGame.Commands;

public static class CreateWordsChainGame
{
    public class Command : IRequest<WordsChainGameOverviewDto>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Topic { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, WordsChainGameOverviewDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppDbContext _context;
        private readonly IWordsChainGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext context, IWordsChainGameNotificationService notificationService, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<WordsChainGameOverviewDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var creatorResult = await _userRepository.GetUsersFromGroupAsync(request.GroupId, new[] { request.UserId }, cancellationToken);

            var sender = creatorResult.GetResult().Entities.Single();

            var wordsChainGame = new WordsChainGame
            {
                Topic = request.Topic,
                GroupId = request.GroupId,
                Participants = new List<UserWordsChainGame>
                {
                    new()
                    {
                        UserId = request.UserId,
                        User = sender,
                        IsCreator = true,
                    }
                },
            };

            _context.WordsChainGames.Add(wordsChainGame);

            var dto = _mapper.Map<WordsChainGameOverviewDto>(wordsChainGame);

            await _notificationService.NotifyNewWordsChainGameCreatedAsync(request.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}