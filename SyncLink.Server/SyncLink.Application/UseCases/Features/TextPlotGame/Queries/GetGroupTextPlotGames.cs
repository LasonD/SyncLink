using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Queries;

public static class GetGroupTextPlotGames
{
    public class Query : IRequest<IPaginatedResult<TextPlotGameDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<TextPlotGameDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly IAppDbContext _context;

        public Handler(IMapper mapper, IUserRepository usersRepository, IAppDbContext context)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _context = context;
        }

        public async Task<IPaginatedResult<TextPlotGameDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var games = await _context.TextPlotGames
                .Where(g => g.GroupId == request.GroupId)
                .Include(g => g.Entries)
                .ToListAsync(cancellationToken);

            var dto = _mapper.Map<PaginatedResult<TextPlotGameDto>>(games);

            return dto;
        }
    }
}