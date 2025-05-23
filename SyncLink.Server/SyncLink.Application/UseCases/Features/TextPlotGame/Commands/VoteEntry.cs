﻿using AutoMapper;
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
        public int Score { get; set; }
    }

    public class Handler : IRequestHandler<Command, TextPlotVoteDto>
    {
        private const int VotingTimeLimitSeconds = 15;
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly ITextPlotGameNotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITextPlotGameVotingProgressNotifier _votingNotifier;

        public Handler(ITextPlotGameNotificationService notificationService, IMapper mapper, ITextPlotGameRepository textPlotGameRepository, ITextPlotGameVotingProgressNotifier votingNotifier, IUserRepository userRepository)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _textPlotGameRepository = textPlotGameRepository;
            _votingNotifier = votingNotifier;
            _userRepository = userRepository;
        }

        public async Task<TextPlotVoteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var pendingEntries = (await _textPlotGameRepository.GetPendingEntriesAsync(request.GroupId, request.GameId, cancellationToken)).GetResult().Entities;

            var entry = pendingEntries.SingleOrDefault(e => e.Id == request.EntryId);

            entry = Validate(request, entry);

            var entryWithVotes = (await _textPlotGameRepository.GetByIdAsync<TextPlotEntry>(entry.Id, cancellationToken, include: e => e.Votes)).GetResult();

            var existingVote = entryWithVotes.Votes.SingleOrDefault(v => v.UserId == request.UserId);

            if (existingVote != null)
            {
                return await HandleVoteUpdateAsync(request, existingVote, cancellationToken);
            }

            return await HandleVoteCreationAsync(request, entry, cancellationToken);
        }

        private async Task<TextPlotVoteDto> HandleVoteCreationAsync(Command request, TextPlotEntry entry, CancellationToken cancellationToken)
        {
            var voter = (await _userRepository.GetByIdAsync(request.UserId, cancellationToken)).GetResult();

            if (voter == null)
            {
                throw new RepositoryActionException(RepositoryActionStatus.NotFound, null, typeof(User));
            }

            var vote = new TextPlotVote(voter, entry, request.Comment, request.Score);

            entry.Votes.Add(vote);

            await _textPlotGameRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TextPlotVoteDto>(vote);

            await _notificationService.NotifyVoteReceivedAsync(request.GroupId, dto, cancellationToken);

            _votingNotifier.StartGameTimerIfNotYetStarted(request.GroupId, entry.GameId, TimeSpan.FromSeconds(VotingTimeLimitSeconds));

            return dto;
        }

        private async Task<TextPlotVoteDto> HandleVoteUpdateAsync(Command request, TextPlotVote existingVote, CancellationToken cancellationToken)
        {
            existingVote.Comment = request.Comment;
            existingVote.Score = request.Score;

            await _textPlotGameRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TextPlotVoteDto>(existingVote);

            await _notificationService.NotifyVoteReceivedAsync(request.GroupId, dto, cancellationToken);

            _votingNotifier.StartGameTimerIfNotYetStarted(request.GroupId, request.GameId, TimeSpan.FromSeconds(VotingTimeLimitSeconds));

            return dto;
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

            if (request.Score is < 0 or > 10)
            {
                throw new BusinessException("Score must be between 0 and 10");
            }

            return entry;
        }
    }
}