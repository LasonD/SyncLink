using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Queries;

public static class GetGroupGames
{
    public class Query : IRequest<IList<TextPlotGameDto>>
    {
        public int GroupId { get; set; }
    }

    public class Handler : IRequestHandler<Query, IList<TextPlotGameDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<TextPlotGameDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var games = await _context.TextPlotGames
                .Where(g => g.GroupId == request.GroupId)
                .ToListAsync(cancellationToken);

            var dto = _mapper.Map<List<TextPlotGameDto>>(games);

            return dto;
        }
    }
}