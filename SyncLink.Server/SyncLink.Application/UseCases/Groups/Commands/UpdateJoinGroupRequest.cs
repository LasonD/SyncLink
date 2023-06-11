using AutoMapper;
using MediatR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;

namespace SyncLink.Application.UseCases.Groups.Commands;

public static class UpdateJoinGroupRequest
{
    public class Command : IRequest<GroupJoinRequestDto>
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int JoinRequestId { get; set; }
        public GroupJoinRequestStatus Status { get; set; }
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

        public async Task<GroupJoinRequestDto> Handle(Command command, CancellationToken cancellationToken)
        {
            var isUserGroupAdmin = await _userRepository.IsUserAdminOfGroupAsync(command.UserId, command.GroupId, cancellationToken);

            if (!isUserGroupAdmin)
            {
                throw new BusinessException($"User {command.UserId} is not admin of group {command.GroupId}");
            }

            var group = (await _groupsRepository.GetByIdAsync(command.GroupId, cancellationToken)).GetResult();
            var existingJoinRequest = (await _groupsRepository.GetByIdAsync<GroupJoinRequest>(command.JoinRequestId, cancellationToken)).GetResult();

            if (existingJoinRequest.GroupId != command.GroupId)
            {
                throw new BusinessException($"Join request {command.JoinRequestId} does not belong to group {command.GroupId}");
            }

            switch (command.Status)
            {
                case GroupJoinRequestStatus.Accepted:
                    group.AcceptJoinRequest(existingJoinRequest);
                    break;
                case GroupJoinRequestStatus.Rejected:
                    existingJoinRequest.Status = GroupJoinRequestStatus.Rejected;
                    break;
                default:
                    throw new BusinessException($"Invalid join request status {command.Status}");
            }

            await _groupsRepository.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<GroupJoinRequestDto>(existingJoinRequest);

            await _generalNotificationsService.NotifyJoinGroupRequestCreatedOrUpdatedAsync(command.GroupId, dto, cancellationToken);

            return dto;
        }
    }
}