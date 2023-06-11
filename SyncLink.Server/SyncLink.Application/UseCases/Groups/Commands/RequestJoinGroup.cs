using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Groups.Commands;

public static class RequestJoinGroup
{
    public class Command : IRequest<GroupJoinRequestDto>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string? Message { get; set; }
    }

    public class Handler : IRequestHandler<Command, GroupJoinRequestDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IGeneralNotificationsService _generalNotificationsService;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IGroupsRepository groupsRepository, IUserRepository userRepository, IGeneralNotificationsService generalNotificationsService)
        {
            _mapper = mapper;
            _groupsRepository = groupsRepository;
            _userRepository = userRepository;
            _generalNotificationsService = generalNotificationsService;
        }

        public async Task<GroupJoinRequestDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var isUserInGroup = await _userRepository.IsUserInGroupAsync(request.UserId, request.GroupId, cancellationToken);

            if (isUserInGroup)
            {
                throw new BusinessException($"User {request.UserId} is already a member of group {request.GroupId}");
            }

            var user = (await _userRepository.GetByIdAsync(request.UserId, cancellationToken)).GetResult();
            var group = (await _groupsRepository.GetByIdAsync(request.GroupId, cancellationToken)).GetResult();

            var joinRequest = group.AddJoinRequest(user, request.Message);

            await _groupsRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<GroupJoinRequestDto>(joinRequest);

            await _generalNotificationsService.NotifyJoinGroupRequestCreatedOrUpdatedAsync(request.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}