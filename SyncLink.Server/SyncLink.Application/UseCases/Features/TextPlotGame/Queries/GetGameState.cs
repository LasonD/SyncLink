using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Queries;

public static class GetGameState
{
    public class Query : IRequest<TextPlotGameDto>
    {
        public int GameId { get; set; }
    }

    public class Handler : IRequestHandler<Query, TextPlotGameDto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TextPlotGameDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var game = await _context.TextPlotGames
                .Include(g => g.Entries)
                .ThenInclude(e => e.Votes)
                .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

            var dto = _mapper.Map<TextPlotGameDto>(game);

            return dto;
        }
    }
}