using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Exceptions;
using SyncLink.Application.UseCases.Queries;

namespace SyncLink.Application.UseCases.Features.TextPlotGame.Queries;

public static class GetTextPlotGameEntries
{
    public class Query : QueryWithPagination, IRequest<IPaginatedResult<TextPlotEntryDto>>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<TextPlotEntryDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITextPlotGameRepository _textPlotGameRepository;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IUserRepository userRepository, ITextPlotGameRepository textPlotGameRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _textPlotGameRepository = textPlotGameRepository;
        }

        public async Task<IPaginatedResult<TextPlotEntryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _userRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var entriesResult = await _textPlotGameRepository.GetTextPlotGameEntriesAsync(
                request.GroupId,
                request.GameId,
                new OrderedPaginationQuery<TextPlotEntry>(request.PageNumber, request.PageSize),
                cancellationToken
            );

            var entries = entriesResult.GetResult();

            var dto = _mapper.Map<IPaginatedResult<TextPlotEntryDto>>(entries);

            return dto;
        }
    }
}