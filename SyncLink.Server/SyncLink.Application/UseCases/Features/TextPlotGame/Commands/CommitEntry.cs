using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class CommitEntry
{
    public class Command : IRequest<TextPlotEntryDto>
    {
        public int GameId { get; set; }
        public int GroupId { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotEntryDto>
    {
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(ITextPlotGameNotificationService notificationService, IMapper mapper, ITextPlotGameRepository textPlotGameRepository)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _textPlotGameRepository = textPlotGameRepository;
        }

        public async Task<TextPlotEntryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var pendingWithMostVotesResult = await _textPlotGameRepository.GetPendingEntryWithMostVotesAsync(request.GroupId, request.GameId, cancellationToken);

            var entry = pendingWithMostVotesResult.GetResult();

            entry.IsCommitted = true;

            var dto = _mapper.Map<TextPlotEntryDto>(entry);

            await _textPlotGameRepository.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyEntryCommittedAsync(request.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}