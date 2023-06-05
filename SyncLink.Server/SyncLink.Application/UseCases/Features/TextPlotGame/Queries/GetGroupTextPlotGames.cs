using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
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
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<TextPlotGameDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly ITextPlotGameRepository _textPlotGameRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository, ITextPlotGameRepository textPlotGameRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _textPlotGameRepository = textPlotGameRepository;
        }

        public async Task<IPaginatedResult<TextPlotGameDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var query = new OrderedPaginationQuery<Domain.Features.TextPlotGame.TextPlotGame>(request.PageNumber, request.PageSize);
            query.OrderingExpressions.Add(new OrderingCriteria<Domain.Features.TextPlotGame.TextPlotGame>(x => x.CreationDate, false));
            query.IncludeExpressions.Add(x => x.Creator);

            var gamesResult = await _textPlotGameRepository.GetBySpecificationAsync(query, cancellationToken);

            var games = gamesResult.GetResult();

            var dto = _mapper.Map<PaginatedResult<TextPlotGameDto>>(games);

            return dto;
        }
    }
}