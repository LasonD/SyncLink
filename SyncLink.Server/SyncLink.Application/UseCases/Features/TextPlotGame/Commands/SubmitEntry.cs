using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Commands;

public static class SubmitEntry
{
    public class Command : IRequest<TextPlotEntryDto>
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Text { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, TextPlotEntryDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IMapper _mapper;

        public Handler(ITextPlotGameNotificationService notificationService, IMapper mapper, ITextPlotGameRepository textPlotGameRepository, IUserRepository userRepository)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _textPlotGameRepository = textPlotGameRepository;
            _userRepository = userRepository;
        }

        public async Task<TextPlotEntryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = (await _userRepository.GetUserFromGroupAsync(request.GroupId, request.UserId, cancellationToken)).GetResult();
            var game = (await _textPlotGameRepository.GetByIdAsync(request.GameId, cancellationToken)).GetResult();

            var entry = game.AddEntry(user, request.Text);
           
            await _textPlotGameRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TextPlotEntryDto>(entry);

            await _notificationService.NotifyNewEntryAsync(game.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}