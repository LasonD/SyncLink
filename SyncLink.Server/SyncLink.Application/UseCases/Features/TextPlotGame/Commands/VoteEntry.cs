﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class VoteCommand : IRequest<TextPlotVoteDto>
    {
        public int GameId { get; set; }
        public int EntryId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<VoteCommand, TextPlotVoteDto>
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

        public async Task<TextPlotVoteDto> Handle(VoteCommand request, CancellationToken cancellationToken)
        {
            var entry = await _context.TextPlotEntries.SingleOrDefaultAsync(e => e.GameId == request.GameId && e.Id == request.EntryId, cancellationToken);

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

            var vote = new TextPlotVote(voter, entry);

            _context.TextPlotVotes.Add(vote);

            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.NotifyVoteReceivedAsync(entry.Game.GroupId, vote, cancellationToken);

            var dto = _mapper.Map<TextPlotVoteDto>(vote);

            return dto;
        }
    }
}