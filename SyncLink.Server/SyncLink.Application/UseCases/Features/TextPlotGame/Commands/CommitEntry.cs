using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces.Factories;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class CommitEntry
{
    public class Command : IRequest<TextPlotEntryDto?>
    {
        public int GameId { get; set; }
        public int GroupId { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotEntryDto?>
    {
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(ITextPlotGameNotificationService notificationService, IMapper mapper, ITextPlotGameRepositoryFactory textPlotGameRepositoryFactory)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _textPlotGameRepository = textPlotGameRepositoryFactory.Create();
        }

        public async Task<TextPlotEntryDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var pendingEntriesResult = await _textPlotGameRepository.GetPendingEntriesAsync(request.GroupId, request.GameId, cancellationToken);

            var pendingEntries = pendingEntriesResult.GetResult().Entities?.ToList();

            if (pendingEntries.IsNullOrEmpty())
            {
                await _notificationService.NotifyEntryNotCommittedAsync(request.GroupId, request.GameId, cancellationToken);
                return null;
            }

            var entriesByVotes = pendingEntries!
                .GroupBy(e => e.Votes.Sum(v => v.Score))
                .DistinctBy(g => g.Key)
                .OrderByDescending(g => g.Key)
                .ToList();

            var entryToCommit = HandleEntriesCommitOrDiscard(entriesByVotes);

            await _textPlotGameRepository.SaveChangesAsync(cancellationToken);

            if (entryToCommit == null)
            {
                await _notificationService.NotifyEntryNotCommittedAsync(request.GroupId, request.GameId, cancellationToken);
                await NotifyForDiscardedEntriesAsync(request.GroupId, pendingEntries, cancellationToken);
                return null;
            }

            var dto = _mapper.Map<TextPlotEntryDto>(entryToCommit);
            await _notificationService.NotifyEntryCommittedAsync(request.GroupId, dto, cancellationToken);
            await NotifyForDiscardedEntriesAsync(request.GroupId, pendingEntries, cancellationToken);

            return dto;
        }

        private static TextPlotEntry? HandleEntriesCommitOrDiscard(IReadOnlyCollection<IGrouping<int, TextPlotEntry>> entriesByVotes)
        {
            var entriesWithMostVotesGroup = entriesByVotes.FirstOrDefault();
            var entriesWithMostVotes = entriesWithMostVotesGroup?.ToList();
            var otherEntries = entriesByVotes.Skip(1).ToList();

            var entryToCommit = entriesWithMostVotes?.Count == 1 ? entriesWithMostVotes.First() : null;

            otherEntries.ForEach(g =>
            {
                foreach (var entry in g)
                {
                    entry.IsDiscarded = true;
                }
            });

            if (entryToCommit == null)
            {
                return null;
            }

            entryToCommit.IsCommitted = true;

            foreach (var entry in entriesWithMostVotes!.Where(entry => !entry.IsCommitted))
            {
                entry.IsDiscarded = true;
            }

            return entryToCommit;
        }

        private async Task NotifyForDiscardedEntriesAsync(int groupId, List<TextPlotEntry>? pendingEntries, CancellationToken cancellationToken)
        {
            var discardedEntryIds = pendingEntries?.Where(e => e.IsDiscarded).Select(e => e.Id).ToArray();
            if (discardedEntryIds != null)
            {
                await _notificationService.NotifyEntriesDiscardedAsync(groupId, discardedEntryIds, cancellationToken);
            }
        }
    }
}