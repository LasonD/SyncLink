using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Data.Result.Exceptions;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class VoteEntry
{
    public class Command : IRequest<TextPlotVoteDto>
    {
        public int GameId { get; set; }
        public int GroupId { get; set; }
        public int EntryId { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotVoteDto>
    {
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly IAppDbContext _context;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ITextPlotGameVotingProgressNotifier _votingNotifier;

        public Handler(IAppDbContext context, ITextPlotGameNotificationService notificationService, IMapper mapper, ITextPlotGameRepository textPlotGameRepository, ITextPlotGameVotingProgressNotifier votingNotifier)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
            _textPlotGameRepository = textPlotGameRepository;
            _votingNotifier = votingNotifier;
        }

        public async Task<TextPlotVoteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var pendingEntries = (await _textPlotGameRepository.GetPendingEntriesAsync(request.GroupId, request.GameId, cancellationToken)).GetResult().Entities;

            var entry = pendingEntries.SingleOrDefault(e => e.Id == request.EntryId);

            if (entry == null)
            {
                throw new RepositoryActionException(RepositoryActionStatus.NotFound, null, typeof(TextPlotEntry));
            }

            if (entry.UserId == request.UserId)
            {
                throw new BusinessException("A user cannot vote for his own text entry.");
            }

            var voter = await _context.ApplicationUsers.FindAsync(request.UserId, cancellationToken);

            if (voter == null)
            {
                throw new RepositoryActionException(RepositoryActionStatus.NotFound, null, typeof(User));
            }

            var vote = new TextPlotVote(voter, entry, request.Comment);

            _context.TextPlotVotes.Add(vote);

            await _context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TextPlotVoteDto>(vote);

            await _notificationService.NotifyVoteReceivedAsync(entry.Game.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}