using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Queries;

public static class GetWhiteboards
{
    public class Query : IRequest<IPaginatedResult<WhiteboardDto>>
    {
        public int GroupId { get; init; }
        public int UserId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }

    public class Handler : IRequestHandler<Query, IPaginatedResult<WhiteboardDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _usersRepository;
        private readonly IWhiteboardRepository _whiteboardRepository;

        public Handler(IMapper mapper, IUserRepository usersRepository, IWhiteboardRepository whiteboardRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _whiteboardRepository = whiteboardRepository;
        }

        public async Task<IPaginatedResult<WhiteboardDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _usersRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (!isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is not a member of group {request.GroupId}.");
            }

            var whiteboardsResult = await _whiteboardRepository.GetGroupWhiteboardsAsync(
                request.GroupId, 
                new OrderedPaginationQuery<Whiteboard>(request.PageNumber, request.PageSize), 
                cancellationToken);

            var whiteboards = whiteboardsResult.GetResult();

            var dto = _mapper.Map<PaginatedResult<WhiteboardDto>>(whiteboards);

            return dto;
        }
    }
}