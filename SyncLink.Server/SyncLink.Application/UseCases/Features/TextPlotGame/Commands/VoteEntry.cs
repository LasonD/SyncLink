using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class VoteEntry
{
    public class Command : IRequest<TextPlotVoteDto>
    {
        public int EntryId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotVoteDto>
    {
        private readonly IAppDbContext _context;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext context, ITextPlotGameNotificationService notificationService, IMapper mapper)
        {
            _context = context;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task<TextPlotVoteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var entry = await _context.TextPlotEntries.FindAsync(request.EntryId, cancellationToken);
            var voter = await _context.ApplicationUsers.FindAsync(request.UserId, cancellationToken);

            var vote = new TextPlotVote(voter, entry);

            _context.TextPlotVotes.Add(vote);
            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyVoteReceivedAsync(entry.Game.GroupId, vote, cancellationToken);

            var dto = _mapper.Map<TextPlotVoteDto>(vote);

            return dto;
        }
    }
}