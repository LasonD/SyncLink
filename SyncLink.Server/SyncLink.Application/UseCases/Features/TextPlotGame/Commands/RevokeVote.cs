using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class RevokeVote 
{
    public class Command : IRequest<bool>
    {
        public int GameId { get; set; }
        public int GroupId { get; set; }
        public int EntryId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly ITextPlotGameNotificationService _notificationService;

        public Handler(ITextPlotGameNotificationService notificationService, ITextPlotGameRepository textPlotGameRepository)
        {
            _notificationService = notificationService;
            _textPlotGameRepository = textPlotGameRepository;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var pendingEntries = (await _textPlotGameRepository.GetPendingEntriesAsync(request.GroupId, request.GameId, cancellationToken)).GetResult().Entities;

            var entry = pendingEntries.SingleOrDefault(e => e.Id == request.EntryId);

            entry = Validate(request, entry);

            var entryWithVotes = (await _textPlotGameRepository.GetByIdAsync<TextPlotEntry>(entry.Id, cancellationToken, include: e => e.Votes)).GetResult();

            var existingVote = entryWithVotes.Votes.SingleOrDefault(v => v.UserId == request.UserId);

            if (existingVote != null)
            {
                return await HandleVoteRevocationAsync(request, entryWithVotes, existingVote, cancellationToken);
            }

            return false;
        }

        private async Task<bool> HandleVoteRevocationAsync(Command request, TextPlotEntry entryWithVotes, TextPlotVote existingVote, CancellationToken cancellationToken)
        {
            entryWithVotes.Votes.Remove(existingVote);

            var changes = await _textPlotGameRepository.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyVoteRevokedAsync(request.GroupId, request.GameId, existingVote.Id, cancellationToken);

            return changes > 0;
        }

        private static TextPlotEntry Validate(Command request, TextPlotEntry? entry)
        {
            if (entry == null)
            {
                throw new RepositoryActionException(RepositoryActionStatus.NotFound, null, typeof(TextPlotEntry));
            }

            if (entry.UserId == request.UserId)
            {
                throw new BusinessException("A user cannot vote for his own text entry.");
            }

            return entry;
        }
    }
}