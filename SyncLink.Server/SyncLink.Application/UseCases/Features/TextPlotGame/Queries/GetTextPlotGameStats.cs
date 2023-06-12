using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Exceptions;
using SyncLink.Application.UseCases.Queries;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Queries;

public static class GetTextPlotGameStats
{
    public class Query : QueryWithPagination, IRequest<TextPlotGameStatsDto>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }

    public class Handler : IRequestHandler<Query, TextPlotGameStatsDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITextPlotGameRepository _textPlotGameRepository;

        public Handler(IUserRepository userRepository, ITextPlotGameRepository textPlotGameRepository)
        {
            _userRepository = userRepository;
            _textPlotGameRepository = textPlotGameRepository;
        }

        public async Task<TextPlotGameStatsDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _userRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var game = (await _textPlotGameRepository.GetTextPlotGameCompleteAsync(request.GroupId, request.GameId, cancellationToken)).GetResult();

            var stats = new TextPlotGameStatsDto
            {
                GameId = game.Id,
                GroupId = game.GroupId,
                EntriesCommittedCount = game.Entries.Count,
                WordsCommittedCount = game.Entries.Sum(e => e.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length),
                Topic = game.Topic,
                UserStats = game.Entries.GroupBy(e => e.UserId).Select(g => new TextPlotGameUserStatsDto
                {
                    UserId = g.Key ?? default,
                    Username = g.First().User.UserName,
                    EntriesCommittedCount = g.Count(e => e.IsCommitted),
                    EntriesSubmittedCount = g.Count(),
                    WordsCommittedCount = g.Where(e => e.IsCommitted).Sum(e => e.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length),
                    WordsSubmittedCount = g.Sum(e => e.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length),
                    CommentsReceived = g.SelectMany(e => e.Votes).Where(v => v.Comment != null).Select(v => v.Comment).ToList(),
                    TotalReceivedScore = g.SelectMany(e => e.Votes).Sum(v => v.Score),
                    VotesLeftCount = game.Entries.Count(e => e.UserId == g.Key && !e.IsCommitted),
                }).ToList(),
            };

            return stats;
        }
    }
}